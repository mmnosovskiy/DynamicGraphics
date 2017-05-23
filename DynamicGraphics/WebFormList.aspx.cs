using System;
using System.Collections.Generic;
using DataMethods;
using System.Web.UI.WebControls;
using System.Collections.ObjectModel;

namespace DynamicGraphics
{
    /// <summary>
    /// Код логики для WebFormList.aspx
    /// </summary>
    public partial class WebFormList : System.Web.UI.Page
    {
        ObservableCollection<DataElement> list;
        /// <summary>
        /// Метод, выполняющийся при загрузке страницы WebFormList.aspx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            list = DataProcessor.ReadFilesFromDatabase(); //Получаем список всех строк таблицы БД.
            List<string> listNames = new List<string>();
            foreach (DataElement item in list) //Создаем список из названий функциональных зависимостей.
            {
                listNames.Add(item.EquationNameRus);
            }
            BulletedList1.DataSource = listNames; //Привяываем список название к элементу BulletedList
            BulletedList1.DataBind(); //Метод привязки.
        }
        /// <summary>
        /// Обработчик события нажатия на элемент списка.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BulletedList1_Click(object sender, BulletedListEventArgs e)
        {
            DataElement item = list[e.Index]; //Получаем нажатый элемент.
            Response.Redirect(@"http://dyngraphs.imet-db.ru/Graphics/" + item.ID); //Переход на страницу построения графика.
        }
    }
}