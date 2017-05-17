namespace DataMethods
{
    /// <summary>
    /// Класс для представления экземпляра зависимости.
    /// </summary>
    public class DataElement
    {
        /// <summary>
        /// Автоматически реализуемое свойство для ID элемента.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Автоматически реализуемое свойство для HeadClue.
        /// </summary>
        public int HeadClue { get; set; }
        /// <summary>
        /// Автоматически реализуемое свойство для SingCode.
        /// </summary>
        public string SingCode { get; set; }
        /// <summary>
        /// Автоматически реализуемое свойство для NomProp.
        /// </summary>
        public int NomProp { get; set; }
        /// <summary>
        /// Автоматически реализуемое свойство для названия зависимости на русском языке.
        /// </summary>
        public string EquationNameRus { get; set; }
        /// <summary>
        /// Автоматически реализуемое свойство для названия зависимости на английском языке.
        /// </summary>
        public string EquationNameEng { get; set; }
        /// <summary>
        /// Автоматически реализуемое свойство для уравнения зависимости (на JS).
        /// </summary>
        public string EquationJS { get; set; }
        /// <summary>
        /// Автоматически реализуемое свойство для коэффициентов уравнения (на JS).
        /// </summary>
        public string EquationCoefficientsJS { get; set; }
        /// <summary>
        /// Автоматически реализуемое свойство для минимальной границы X.
        /// </summary>
        public double Xmin { get; set; }
        /// <summary>
        /// Автоматически реализуемое свойство для максимальной границы X.
        /// </summary>
        public double Xmax { get; set; }
        /// <summary>
        /// Автоматически реализуемое свойство для легенды оси X.
        /// </summary>
        public string Xaxis { get; set; }
        /// <summary>
        /// Автоматически реализуемое свойство для единиц измерения X.
        /// </summary>
        public string Xmeasure { get; set; }
        /// <summary>
        /// Автоматически реализуемое свойство для легенды оси Y.
        /// </summary>
        public string Yaxis { get; set; }
        /// <summary>
        /// Автоматически реализуемое свойство для единиц измерения X.
        /// </summary>
        public string Ymeasure { get; set; }
        /// <summary>
        /// Автоматически реализуемое свойство для параметров (на XML).
        /// </summary>
        public string Params { get; set; }
        /// <summary>
        /// Автоматически реализуемое свойство для комментария к зависимости.
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// Автоматически реализуемое свойство для BKNumber.
        /// </summary>
        public int BKNumber { get; set; }
        /// <summary>
        /// Метод для создания копии текущего элемента DataElement.
        /// </summary>
        /// <returns></returns>
        public DataElement Copy()
        {
            DataElement temp = new DataElement();
            temp.ID = ID;
            temp.HeadClue = HeadClue;
            temp.SingCode = SingCode;
            temp.NomProp = NomProp;
            temp.EquationNameRus = EquationNameRus;
            temp.EquationNameEng = EquationNameEng;
            temp.EquationJS = EquationJS;
            temp.EquationCoefficientsJS = EquationCoefficientsJS;
            temp.Xmin = Xmin;
            temp.Xmax = Xmax;
            temp.Xaxis = Xaxis;
            temp.Xmeasure = Xmeasure;
            temp.Yaxis = Yaxis;
            temp.Ymeasure = Ymeasure;
            temp.Params = Params;
            temp.Comment = Comment;
            temp.BKNumber = BKNumber;
            return temp;
        }
        /// <summary>
        /// Метод для поиска соответсвия строки в текстовых свойствах.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool Contains(string text)
        {
            return SingCode.Contains(text)
                || EquationNameRus.Contains(text)
                || EquationNameEng.Contains(text)
                || EquationJS.Contains(text)
                || EquationCoefficientsJS.Contains(text)
                || Params.Contains(text)
                || Xaxis.Contains(text)
                || Yaxis.Contains(text)
                || Xmeasure.Contains(text)
                || Ymeasure.Contains(text)
                || Comment.Contains(text);
        }
    }
}
