using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Xml;
using System;
using System.Globalization;

namespace DataMethods
{
    /// <summary>
    /// Статический класс с методами.
    /// </summary>
    public static class DataProcessor
    {
        /// <summary>
        /// Коллекция элементов базы данных.
        /// </summary>
        private static ObservableCollection<DataElement> _elements;
        /// <summary>
        /// Строка подключения к базе данных.
        /// </summary>
        //private readonly static string connectionString = @"Data Source=LUCKY13\SQLEXPRESS;Initial Catalog=Graphics;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private readonly static string connectionString = @"Data Source=logical-nosovskiy.database.windows.net;Initial Catalog=Graphics;Integrated Security=False;User ID=Lucky13;Password=1235846Qq;Connect Timeout=15;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        /// <summary>
        /// Строка с именем хранимой процедуры.
        /// </summary>
        private static string sqlExpression;
        /// <summary>
        /// Свойство для получения коллекции элементов базы данных.
        /// </summary>
        public static ObservableCollection<DataElement> Elements
        {
            get
            {
                return _elements;
            }
        }
        /// <summary>
        /// Метод для проверки строки параметра на значение null.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string CheckForNull(string param)
        {
            if (param != null) return param;
            return string.Empty;
        }
        /// <summary>
        /// Метод для добавления элемента в коллекцию _elements.
        /// </summary>
        /// <param name="item"></param>
        public static void AddElement(DataElement item)
        {
            _elements.Add(item);
        }
        /// <summary>
        /// Метод для удаления элемента из коллекции и из базы данных.
        /// </summary>
        /// <param name="elements"></param>
        public static void DeleteElement(System.Collections.IList elements)
        {
            object[] els = new object[elements.Count];
            elements.CopyTo(els, 0); //копирование выделенных элементов в массив.
            sqlExpression = "sp_DeleteElement"; //Название хранимой процедуры.
            using (SqlConnection connection = new SqlConnection(connectionString)) //Подключение к базе данных.
            {
                connection.Open(); //Открытие подключения.
                //Создание объекта хранимой процедуры.
                SqlCommand command = new SqlCommand(sqlExpression, connection); 
                command.CommandType = System.Data.CommandType.StoredProcedure;
                //Создание параметров процедуры.
                SqlParameter idP = new SqlParameter
                {
                    ParameterName = "@id"
                };
                command.Parameters.Add(idP); //Добавление параметров процедуры.
                for (int i = 0; i < els.Length; i++)
                {
                    idP.Value = ((DataElement)els[i]).ID;//Установка значения параметра
                    command.ExecuteNonQuery(); //Выполнение процедуры.
                    _elements.Remove((DataElement)els[i]); //Удаление элемента из коллекции.
                }
            }
        }
        /// <summary>
        /// Метод для добавления элемента в коллекцию и в базу данных.
        /// </summary>
        /// <param name="element"></param>
        public static void AddElementToDB(DataElement element)
        {
            
            sqlExpression = "sp_InsertElement"; //Название хранимой процедуры.

            using (SqlConnection connection = new SqlConnection(connectionString)) //Подключение к базе данных.
            {
                connection.Open(); //Открытие подключения.
                //Создание объекта хранимой процедуры.
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                //Создание и добавление параметров процедуры.
                SqlParameter headClueP = new SqlParameter
                {
                    ParameterName = "@headClue",
                    Value = element.HeadClue
                };
                command.Parameters.Add(headClueP);
                SqlParameter singCodeP = new SqlParameter
                {
                    ParameterName = "@singCode",
                    Value = CheckForNull(element.SingCode)
                };
                command.Parameters.Add(singCodeP);
                SqlParameter nomPropP = new SqlParameter
                {
                    ParameterName = "@nomProp",
                    Value = element.NomProp
                };
                command.Parameters.Add(nomPropP);
                SqlParameter equationNameRusP = new SqlParameter
                {
                    ParameterName = "@equationNameRus",
                    Value = CheckForNull(element.EquationNameRus)
                };
                command.Parameters.Add(equationNameRusP);
                SqlParameter equationNameEngP = new SqlParameter
                {
                    ParameterName = "@equationNameEng",
                    Value = CheckForNull(element.EquationNameEng)
                };
                command.Parameters.Add(equationNameEngP);
                SqlParameter equationJS = new SqlParameter
                {
                    ParameterName = "@equationJS",
                    Value = CheckForNull(element.EquationJS)
                };
                command.Parameters.Add(equationJS);
                SqlParameter equationCoefficientsJS = new SqlParameter
                {
                    ParameterName = "@equationCoefficientsJS",
                    Value = CheckForNull(element.EquationCoefficientsJS)
                };
                command.Parameters.Add(equationCoefficientsJS);
                SqlParameter xminP = new SqlParameter
                {
                    ParameterName = "@xmin",
                    Value = element.Xmin
                };
                command.Parameters.Add(xminP);
                SqlParameter xmaxP = new SqlParameter
                {
                    ParameterName = "@xmax",
                    Value = element.Xmax
                };
                command.Parameters.Add(xmaxP);
                SqlParameter xaxisP = new SqlParameter
                {
                    ParameterName = "@xaxis",
                    Value = CheckForNull(element.Xaxis)
                };
                command.Parameters.Add(xaxisP);
                SqlParameter xmeasureP = new SqlParameter
                {
                    ParameterName = "@xmeasure",
                    Value = CheckForNull(element.Xmeasure)
                };
                command.Parameters.Add(xmeasureP);
                SqlParameter yaxisP = new SqlParameter
                {
                    ParameterName = "@yaxis",
                    Value = CheckForNull(element.Yaxis)
                };
                command.Parameters.Add(yaxisP);
                SqlParameter ymeasureP = new SqlParameter
                {
                    ParameterName = "@ymeasure",
                    Value = CheckForNull(element.Ymeasure)
                };
                command.Parameters.Add(ymeasureP);
                SqlParameter paramsP = new SqlParameter
                {
                    ParameterName = "@params",
                    Value = CheckForNull(element.Params)
                };
                command.Parameters.Add(paramsP);
                SqlParameter commentP = new SqlParameter
                {
                    ParameterName = "@comment",
                    Value = CheckForNull(element.Comment)
                };
                command.Parameters.Add(commentP);
                SqlParameter bknumberP = new SqlParameter
                {
                    ParameterName = "@bknumber",
                    Value = element.BKNumber
                };
                command.Parameters.Add(bknumberP);
                element.ID = (int)command.ExecuteScalar(); //Выполнение процедуры.
            }
        }
        /// <summary>
        /// Метод для обновления свойств элемента в коллекции и в базе данных.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="pos"></param>
        public static void UpdateElement(DataElement element, int pos)
        {
            sqlExpression = "sp_UpdateElement"; //Название хранимой процедуры.

            _elements[pos] = element;

            using (SqlConnection connection = new SqlConnection(connectionString)) //Подключение к базе данных.
            {
                connection.Open(); //Открытие подключения.
                //Создание объекта хранимой процедуры.
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                //Создание и добавление параметров процедуры.
                SqlParameter idP = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = element.ID
                };
                command.Parameters.Add(idP);
                SqlParameter headClueP = new SqlParameter
                {
                    ParameterName = "@headClue",
                    Value = element.HeadClue
                };
                command.Parameters.Add(headClueP);
                SqlParameter singCodeP = new SqlParameter
                {
                    ParameterName = "@singCode",
                    Value = CheckForNull(element.SingCode)
                };
                command.Parameters.Add(singCodeP);
                SqlParameter nomPropP = new SqlParameter
                {
                    ParameterName = "@nomProp",
                    Value = element.NomProp
                };
                command.Parameters.Add(nomPropP);
                SqlParameter equationNameRusP = new SqlParameter
                {
                    ParameterName = "@equationNameRus",
                    Value = CheckForNull(element.EquationNameRus)
                };
                command.Parameters.Add(equationNameRusP);
                SqlParameter equationNameEngP = new SqlParameter
                {
                    ParameterName = "@equationNameEng",
                    Value = CheckForNull(element.EquationNameEng)
                };
                command.Parameters.Add(equationNameEngP);
                SqlParameter equationJS = new SqlParameter
                {
                    ParameterName = "@equationJS",
                    Value = CheckForNull(element.EquationJS)
                };
                command.Parameters.Add(equationJS);
                SqlParameter equationCoefficientsJS = new SqlParameter
                {
                    ParameterName = "@equationCoefficientsJS",
                    Value = CheckForNull(element.EquationCoefficientsJS)
                };
                command.Parameters.Add(equationCoefficientsJS);
                SqlParameter xminP = new SqlParameter
                {
                    ParameterName = "@xmin",
                    Value = element.Xmin
                };
                command.Parameters.Add(xminP);
                SqlParameter xmaxP = new SqlParameter
                {
                    ParameterName = "@xmax",
                    Value = element.Xmax
                };
                command.Parameters.Add(xmaxP);
                SqlParameter xaxisP = new SqlParameter
                {
                    ParameterName = "@xaxis",
                    Value = CheckForNull(element.Xaxis)
                };
                command.Parameters.Add(xaxisP);
                SqlParameter xmeasureP = new SqlParameter
                {
                    ParameterName = "@xmeasure",
                    Value = CheckForNull(element.Xmeasure)
                };
                command.Parameters.Add(xmeasureP);
                SqlParameter yaxisP = new SqlParameter
                {
                    ParameterName = "@yaxis",
                    Value = CheckForNull(element.Yaxis)
                };
                command.Parameters.Add(yaxisP);
                SqlParameter ymeasureP = new SqlParameter
                {
                    ParameterName = "@ymeasure",
                    Value = CheckForNull(element.Ymeasure)
                };
                command.Parameters.Add(ymeasureP);
                SqlParameter paramsP = new SqlParameter
                {
                    ParameterName = "@params",
                    Value = CheckForNull(element.Params)
                };
                command.Parameters.Add(paramsP);
                SqlParameter commentP = new SqlParameter
                {
                    ParameterName = "@comment",
                    Value = CheckForNull(element.Comment)
                };
                command.Parameters.Add(commentP);
                SqlParameter bknumberP = new SqlParameter
                {
                    ParameterName = "@bknumber",
                    Value = element.BKNumber
                };
                command.Parameters.Add(bknumberP);
                command.ExecuteNonQuery(); //Выполнение процедуры.
            }
        }
        /// <summary>
        /// Метод для получения коллекции элементов из базы данных.
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<DataElement> ReadFilesFromDatabase()
        {
            sqlExpression = "sp_GetData"; //Название хранимой процедуры.

            ObservableCollection<DataElement> list = new ObservableCollection<DataElement>();
            using (SqlConnection connection = new SqlConnection(connectionString)) //Подключение к базе данных.
            {
                connection.Open();//Открытие подключения.
                //Создание объекта хранимой процедуры.
                SqlCommand command = new SqlCommand(sqlExpression, connection);                
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader(); //Создание объекта для чтения данных из базы данных.
                //Чтение данных.
                while (reader.Read())
                {
                    DataElement element = new DataElement();
                    element.ID = reader.GetInt32(0);
                    if (!reader.IsDBNull(1)) element.HeadClue = reader.GetInt32(1);
                    else element.HeadClue = 0;
                    if (!reader.IsDBNull(2)) element.SingCode = reader.GetString(2);
                    else element.SingCode = string.Empty;
                    if (!reader.IsDBNull(3)) element.NomProp = reader.GetInt32(3);
                    else element.NomProp = 0;
                    if (!reader.IsDBNull(4)) element.EquationNameRus = reader.GetString(4);
                    else element.EquationNameRus = string.Empty;
                    element.EquationNameEng = reader.GetString(5);
                    element.EquationJS = reader.GetString(6);
                    if (!reader.IsDBNull(7)) element.EquationCoefficientsJS = reader.GetString(7);
                    else element.EquationCoefficientsJS = string.Empty;
                    element.Xmin = reader.GetDouble(8);
                    element.Xmax = reader.GetDouble(9);
                    if (!reader.IsDBNull(10)) element.Xaxis = reader.GetString(10);
                    else element.Xaxis = string.Empty;
                    if (!reader.IsDBNull(11)) element.Xmeasure = reader.GetString(11);
                    else element.Xmeasure = string.Empty;
                    if (!reader.IsDBNull(12)) element.Yaxis = reader.GetString(12);
                    else element.Yaxis = string.Empty;
                    if (!reader.IsDBNull(13)) element.Ymeasure = reader.GetString(13);
                    else element.Ymeasure = string.Empty;
                    if (!reader.IsDBNull(14)) element.Params = reader.GetString(14);
                    else element.Params = string.Empty;
                    if (!reader.IsDBNull(15)) element.Comment = reader.GetString(15);
                    else element.Comment = string.Empty;
                    if (!reader.IsDBNull(16)) element.BKNumber = reader.GetInt32(16);
                    else element.BKNumber = 0;

                    list.Add(element); //Добавление элемента в коллекцию.
                }
            }
            return _elements = list;

        }
        /// <summary>
        /// Метод для получения элемента из базы данных по ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataElement GetElemByID(int id)
        {
            sqlExpression = "sp_GetElementById"; //Название хранимой процедуры.

            DataElement element = new DataElement();

            using (SqlConnection connection = new SqlConnection(connectionString)) //Подключение к базе данных.
            {
                connection.Open(); //Открытие подключения.
                //Создание объекта хранимой процедуры.
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                //Создание и добавление параметров процедуры.
                SqlParameter idP = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = id
                };
                command.Parameters.Add(idP);
                SqlDataReader reader = command.ExecuteReader(); //Создание объекта для чтения данных из базы данных.
                //Чтение данных.
                reader.Read();
                element.ID = reader.GetInt32(0);
                if (!reader.IsDBNull(1)) element.HeadClue = reader.GetInt32(1);
                else element.HeadClue = 0;
                if (!reader.IsDBNull(2)) element.SingCode = reader.GetString(2);
                else element.SingCode = string.Empty;
                if (!reader.IsDBNull(3)) element.NomProp = reader.GetInt32(3);
                else element.NomProp = 0;
                if (!reader.IsDBNull(4)) element.EquationNameRus = reader.GetString(4);
                else element.EquationNameRus = string.Empty;
                element.EquationNameEng = reader.GetString(5);
                element.EquationJS = reader.GetString(6);
                if (!reader.IsDBNull(7)) element.EquationCoefficientsJS = reader.GetString(7);
                else element.EquationCoefficientsJS = string.Empty;
                element.Xmin = reader.GetDouble(8);
                element.Xmax = reader.GetDouble(9);
                if (!reader.IsDBNull(10)) element.Xaxis = reader.GetString(10);
                else element.Xaxis = string.Empty;
                if (!reader.IsDBNull(11)) element.Xmeasure = reader.GetString(11);
                else element.Xmeasure = string.Empty;
                if (!reader.IsDBNull(12)) element.Yaxis = reader.GetString(12);
                else element.Yaxis = string.Empty;
                if (!reader.IsDBNull(13)) element.Ymeasure = reader.GetString(13);
                else element.Ymeasure = string.Empty;
                if (!reader.IsDBNull(14)) element.Params = reader.GetString(14);
                else element.Params = string.Empty;
                if (!reader.IsDBNull(15)) element.Comment = reader.GetString(15);
                else element.Comment = string.Empty;
                if (!reader.IsDBNull(16)) element.BKNumber = reader.GetInt32(16);
                else element.BKNumber = 0;
            }

            return element;
        }
        /// <summary>
        /// Метод для преобразования XML строки параметров в коллекцию параметров.
        /// </summary>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static List<Param> GetParams(string xmlString)
        {
            List<Param> list = new List<Param>(); //Коллекция параметров.
            XmlDocument xDoc = new XmlDocument(); //Объект для чтения XML строки.
            try
            {
                xDoc.LoadXml(xmlString); //Получение XML строки
                //Точка в качестве разделителя в числах.
                NumberFormatInfo provider = new NumberFormatInfo();
                provider.NumberDecimalSeparator = ".";
                //Чтение данных из XML строки.
                XmlElement xRoot = xDoc.DocumentElement;
                foreach (XmlElement xnode in xRoot)
                {
                    Param par = new Param();
                    if (xnode.Attributes.Count > 0)
                    {
                        par.Name = xnode.GetAttribute("name");
                        par.Min = Convert.ToDouble(xnode.GetAttribute("min"), provider);
                        par.Max = Convert.ToDouble(xnode.GetAttribute("max"), provider);
                        par.Description = xnode.GetAttribute("description");
                        if (par.Min >= par.Max) throw new Exception(); //Выбрасывает исключение, если параметр некорректно задан.
                    }

                    list.Add(par); //Добавление параметра в коллекцию.
                }
            }
            catch
            {
                list = null; //Если вылетело исключение, то list = null.
            }
            return list;
        }
        /// <summary>
        /// Структура для представления данных о параметре зависимости.
        /// </summary>
        public struct Param
        {
            /// <summary>
            /// Имя параметра.
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// Минимальное значение параметра.
            /// </summary>
            public double Min { get; set; }
            /// <summary>
            /// Максимальное значение параметра.
            /// </summary>
            public double Max { get; set; }
            /// <summary>
            /// Описание параметра.
            /// </summary>
            public string Description { get; set; }
        }
        /// <summary>
        /// Метод для проверки валидности XML строки параметров.
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        public static bool ParamValidation(string par)
        {
            bool res;
            try
            {
                if (par == string.Empty) return true; //Если строка пустая, то нет параметров.
                List<Param> list = GetParams(par); //Получение коллекции параметров из строки par.
                //Проверка на количество параметров. Не должно быть больше 4.
                if (list.Count <= 4) 
                {
                    res = true;
                }
                else res = false;
            }
            catch
            {
                res = false; //Если вылетело исключение, значит параметры заданы некорректно.
            }
            return res;
        }
        /// <summary>
        /// Получение элемента для построения графика (в веб-приложении).
        /// </summary>
        /// <param name="ElemID"></param>
        /// <returns></returns>
        public static DataElement ElementLoad(int ElemID)
        {
            DataElement element = GetElemByID(ElemID); //Получение элемента из базы данных по ID.
            //Форматирование строки коэффициентов элемента.
            string[] arr = element.EquationCoefficientsJS.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            element.EquationCoefficientsJS = string.Empty;
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = "var " + arr[i] + ";\n";
                element.EquationCoefficientsJS += arr[i];
            }
            return element;
        }
    }
}
