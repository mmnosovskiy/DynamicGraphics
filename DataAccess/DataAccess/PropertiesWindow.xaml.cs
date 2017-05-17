using System;
using System.Collections.Generic;
using System.Windows;
using DataMethods;
using System.Text.RegularExpressions;
using System.Globalization;

namespace DataAccess
{
    /// <summary>
    /// Логика взаимодействия для PropertiesWindow.xaml
    /// </summary>
    public partial class PropertiesWindow : Window
    {
        /// <summary>
        /// Автоматически реализуемое свойство, хранящее ссылку на объект класса DataElement, отвечающий данному окну.
        /// </summary>
        public DataElement Data { get; set; }
        /// <summary>
        /// Позиция элемента Data в списке элементов.
        /// </summary>
        int Position { get; set; }
        /// <summary>
        /// Флаг, определяющий создается новый элемент или обновляется существующий.
        /// </summary>
        bool NewEl { get; set; }
        /// <summary>
        /// Конструктор класса PropertiesWindow без параметров.
        /// </summary>
        public PropertiesWindow()
        {
            NewEl = true;
            Data = new DataElement();
            DataContext = Data;
            InitializeComponent();
        }
        /// <summary>
        /// Конструктор класса PropertiesWindow с 2 параметрами.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pos"></param>
        public PropertiesWindow(DataElement data, int pos)
        {
            Data = data;
            DataContext = Data;
            Position = pos;
            NewEl = false;
            InitializeComponent();
        }
        /// <summary>
        /// Обработчик события нажатия кнопки OK.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            if (NumberValidation())
            {
                MessageBox.Show("Числовые поля заданы некорректно!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (SingCode.Text == "" || EquationNameRus.Text == "" || EquationJS.Text == "")
            {
                MessageBox.Show("Не все обязательные поля заполнены!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (Convert.ToDouble(Xmin.Text, provider) >= Convert.ToDouble(Xmax.Text, provider))
            {
                MessageBox.Show("Некорректный промежуток!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!DataProcessor.ParamValidation(Params.Text))
            {
                MessageBox.Show("Некорректно введены параметры!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!ParValidation())
            {
                MessageBox.Show("Некорректно задано уравнение! (с коэффициентами и параметрами)", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Close();

                if (NewEl)
                {
                    DataProcessor.AddElementToDB(Data);
                    DataProcessor.AddElement(Data);
                }
                else DataProcessor.UpdateElement(Data, Position);
            }
            
        }
        /// <summary>
        /// Проверка корректности числовых полей.
        /// </summary>
        /// <returns></returns>
        public bool NumberValidation()
        {
            bool res = false;
            try
            {
                int.Parse(HeadClue.Text);
                int.Parse(NomProp.Text);
                double.Parse(Xmin.Text);
                double.Parse(Xmax.Text);
                int.Parse(BKNumber.Text);
            }
            catch
            {
                res = true;
            }
            return res;
        }
        /// <summary>
        /// Проверка корректности ввода уравнения зависимости, параметров и коэффициентов.
        /// </summary>
        /// <returns></returns>
        public bool ParValidation()
        {
            CSharpExecuter cs = new CSharpExecuter(); //Создание объекта класса для исполнения кода.
            string code = ""; //Строка с кодом.
            char[] separator = { ';' };
            string equation = EquationJS.Text; //Строка с уравнением.
            string coef = EquationCoefficientsJS.Text; //Строка с коэффициентами.
            List<DataProcessor.Param> list = DataProcessor.GetParams(Params.Text); //Коллекция параметров зависимости.
            string paramsStr = ""; //Строка параметров.
            //Создается строка из коллекции параметров.
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    paramsStr += "var " + list[i].Name + " = 0;\n";
                }
            }
            //Перевод из JS в C#.
            string pattern = @"(?<=Math\.)[a-z]";
            equation = (Regex.Replace(equation, pattern, x => x.ToString().ToUpper())).ToString();
            pattern = @"(?<=Math\.)Ceil";
            equation = (Regex.Replace(equation, pattern, x => "Ceiling")).ToString();
            //Форматирование строки коэффициентов.
            string[] coefArr = coef.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            coef = "";
            for (int i = 0; i < coefArr.Length; i++)
            {
                coefArr[i] = "var " + coefArr[i] + ";\n";
                coef += coefArr[i];
            }
            //Создание строки кода.
            code = paramsStr + coef + "double X = 1;\nvar res = " + equation + ";";
            cs.FormatSources(code); //Форматирование кода.
            cs.Execute(); //Выполнение кода.

            return cs.IsSuccessful; //Удачно ли был выполнен кода?
        }

    }
}
