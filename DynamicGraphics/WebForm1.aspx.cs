using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DataMethods;
using System.Globalization;
using System.Web.Routing;

namespace DynamicGraphics
{
    /// <summary>
    /// Класс веб формы.
    /// </summary>
    public partial class WebForm1 : System.Web.UI.Page
    {
        /// <summary>
        /// ID елемента.
        /// </summary>
        public static int ElemID;
        /// <summary>
        /// Элемент, график которого строится.
        /// </summary>
        public static DataElement element;
        /// <summary>
        /// Формат строки параметра в разметке.
        /// </summary>
        readonly static string paramFormat = "var {0} = Number(document.getElementById('TextBox{1}').value)";
        /// <summary>
        /// Формат создания слайдера в разметке.
        /// </summary>
        readonly static string sliderFormat = @"
                $('#slider{0}').slider({{
                value: {1},        
                slide: setInputsFromSlider{0},   
                create: setInputsFromSlider_{0},
                min: {1},
                max: {2},
                step: 0.01
            }})" + "\nfunction setInputsFromSlider{0}() {{\n$('#TextBox{0}').val($('#slider{0}').slider(\"value\"));\n validError{0} = false;\n$('#TextBox{0}').css('border', '');\n if (!validError0 && !validError1 && !validError2 && !validError3) drawChart();\n }}" + "\nfunction setInputsFromSlider_{0}() {{\n$('#TextBox{0}').val($('#slider{0}').slider(\"value\"));\n}}";
        /// <summary>
        /// Формат создания методов валидации в разметке.
        /// </summary>
        readonly static string validationFormat = @"function validation{0}() {{
            var min{0} = {1};
            var max{0} = {2};
            var val{0} = Number(document.getElementById('TextBox{0}').value);
            if (val{0} != NaN && min{0} <= val{0} && max{0} >= val{0}) {{
                $('#TextBox{0}').css('border', '');
                $('#slider{0}').slider(" + "\"value\"" + @", val{0});
                validError{0} = false;
                if ({3}) drawChart();
    }}
            else {{
                    validError{0} = true;
                $('#TextBox{0}').css({{
        'border': '3px solid red'
                }})
            }}
        }}";
        readonly static string validFormat = @"!validError{0} && !validError{1} && !validError{2}";

        /// <summary>
        /// Метод, выполняющийся при загрузке страницы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Установка точки в качестве десятичного разделителя.
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            Label[] lab = new Label[] { Label0, Label1, Label2, Label3 }; //Массив Label[] для параметров.
            TextBox[] txtbx1 = new TextBox[] { TextBox0, TextBox1, TextBox2, TextBox3 }; //Массив TextBox[] для параметров.

            //Получение ID элемента из роута.
            Route myRoute = RouteData.Route as Route;
            if (myRoute != null && myRoute.Url == "{id}")
            {
                string id = RouteData.Values["id"].ToString();
                ElemID = Convert.ToInt32(id);
            }
            //Получение элемента из базы данных по ID.
            element = DataProcessor.ElementLoad(ElemID);
            //Подготовка строки JS-скрипта.
            List<DataProcessor.Param> list = DataProcessor.GetParams(element.Params);
            string paramStr = string.Empty;
            string sliderScript = string.Empty;
            string validationScript = string.Empty;
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    paramStr += string.Format(paramFormat, list[i].Name, i) + ";\n";
                    switch (i)
                    {
                        case 0:
                            validationScript += string.Format(provider, validationFormat, i, list[i].Min, list[i].Max, string.Format(validFormat, 1, 2, 3)) + ";\n";
                            break;
                        case 1:
                            validationScript += string.Format(provider, validationFormat, i, list[i].Min, list[i].Max, string.Format(validFormat, 0, 2, 3)) + ";\n";
                            break;
                        case 2:
                            validationScript += string.Format(provider, validationFormat, i, list[i].Min, list[i].Max, string.Format(validFormat, 0, 1, 3)) + ";\n";
                            break;
                        case 3:
                            validationScript += string.Format(provider, validationFormat, i, list[i].Min, list[i].Max, string.Format(validFormat, 0, 1, 2)) + ";\n";
                            break;
                    }
                    sliderScript += string.Format(provider, sliderFormat, i, list[i].Min, list[i].Max) + "\n";
                    lab[i].Visible = lab[i].Enabled = true;
                    lab[i].Text = list[i].Name + " = ";
                    txtbx1[i].Visible = txtbx1[i].Enabled = true;
                }
            }
            //Формирование строки скрипта.
            string scriptText = "\n<script type=\"text/javascript\">\nvar xmin =" + element.Xmin.ToString(CultureInfo.InvariantCulture) + ";\nvar xmax = " + element.Xmax.ToString(CultureInfo.InvariantCulture) + ";\nvar title = '" + element.EquationNameEng + "';\nvar yax = '" + element.Yaxis + "';\nfunction f(X) {\n" + paramStr + "\n" + element.EquationCoefficientsJS + "\nreturn " + element.EquationJS + ";\n}\n $(function () {\n" + sliderScript + "\n});\n" + validationScript + "\n</script>";
            //Добавление скрипта в разметку страницы.
            NewScript.Text = scriptText;
            //Добавление загаловка в разметку страницы.
            TitleLit.Text = element.EquationNameRus;
            //Добавление формулы зависимости под загаловком.
            Func.Text = element.EquationJS;
        }
    }
}