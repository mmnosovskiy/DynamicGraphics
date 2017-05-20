using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Diagnostics;
using DataMethods;


namespace DataAccess
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {   
        /// <summary>
        /// Конструктор класса MainWindow
        /// </summary>   
        public MainWindow()
        {
            InitializeComponent();
            dataGrid.ItemsSource = DataProcessor.ReadFilesFromDatabase(); //Чтение данных из базы данных.
        }
        /// <summary>
        /// Обработчик события двойного клика на DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dataGrid.SelectedItem != null) //Выбран ли элемент?
            {
                //Открытие диалогового окна параметров.
                PropertiesWindow prWindow = new PropertiesWindow(((DataElement)dataGrid.SelectedItem).Copy(), dataGrid.SelectedIndex);
                prWindow.ShowDialog();
            }
        }
        /// <summary>
        /// Обработчик события изменения выбранного элемента в DataGrid. Меняет свойство Enabled кнопки "Нарисовать".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItem != null) Draw_button.IsEnabled = true;
            else Draw_button.IsEnabled = false;
        }
        /// <summary>
        /// Обработчик события нажатия кнопки "Добавить".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            //Открытие диалогового окна параметров.
            PropertiesWindow prWindow = new PropertiesWindow();
            prWindow.ShowDialog();
        }
        /// <summary>
        /// Обработчик события нажатия кнопки "Изменить".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null) //Выбран ли элемент?
            {
                //Открытие диалогового окна параметров.
                PropertiesWindow prWindow = new PropertiesWindow(((DataElement)dataGrid.SelectedItem).Copy(), dataGrid.SelectedIndex);
                prWindow.ShowDialog();
            }            
            else
            {
                MessageBox.Show("Нет выбранного элемента", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// Обработчик события нажатия кнопки "Удалить" или "Delete" на клавиатуре.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count > 0) //Выбраны ли элементы?
            {
                if (MessageBox.Show("Вы уверены? ", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    DataProcessor.DeleteElement(dataGrid.SelectedItems); //Удаление элементов из коллекции и из базы данных.
                }
            }
            else
            {
                MessageBox.Show("Нет выбранных элементов", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// Обработчик события нажатия кнопки "Нарисовать".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDraw_Click(object sender, RoutedEventArgs e)
        {
            string url = @"http://dyngraphs.imet-db.ru/Graphics/" + ((DataElement)dataGrid.SelectedItem).ID; //Формирование роута.
            
            Process.Start(url); //Переход в веб-приложение.
        }
        /// <summary>
        /// Обработчик события изменения текста в текстовом поле "Найти:". Изменяет список отображаемых элементов в DataGrid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchTextBox.Text == string.Empty)
            {
                dataGrid.ItemsSource = DataProcessor.Elements;
                FoundNumber.Text = "";
            }
            else
            {
                string text = SearchTextBox.Text;
                List<DataElement> searchList = new List<DataElement>();
                foreach (DataElement item in DataProcessor.Elements)
                {
                    if (item.Contains(text)) searchList.Add(item.Copy());
                }
                dataGrid.ItemsSource = searchList;
                FoundNumber.Text = "Найдено эл-тов: " + searchList.Count;
            }
        }
    }
}
