using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task_1
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        MainWindow mainWindow;

        public MainPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        /// <summary>
        /// Открыть страницу добавления клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenNewCustomerPage(object sender, RoutedEventArgs e)
        {
            mainWindow.NewCustomerPage();
        }

        /// <summary>
        /// Открыть страницу информации о клиенте
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenCustomerAccountPage(object sender, RoutedEventArgs e)
        {
            if (CustomersView.SelectedItem != null)
            {
                mainWindow.CustomerAccountPage();
            }
            else
            {
                MessageBox.Show("Клиент не выбран");
            }
        }

        /// <summary>
        /// Проверка выбраного клиента и закрытие счёта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseAccount(object sender, RoutedEventArgs e)
        {
            if(CustomersView.SelectedItem != null)
            {
                mainWindow.DeleteCustomer(); 
            }
            else
            {
                MessageBox.Show("Клиент не выбран");
            }
        }
    }
}
