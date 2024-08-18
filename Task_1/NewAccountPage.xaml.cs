using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для NewAccountPage.xaml
    /// </summary>
    public partial class NewAccountPage : Page
    {
        MainWindow mainWindow;

        public bool _accountType;
        public int _customerId;
        public NewAccountPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void CreateNewAccount(object sender, RoutedEventArgs e)
        {
            if(CheckEmptyInput())
            {
                long balance = long.Parse(AccountBalance.Text);
                mainWindow.AddNewAccount(_customerId, balance, _accountType);
                mainWindow.CustomerAccountPage();
            }
        }

        private bool CheckEmptyInput()
        {
            long input;

            if (!long.TryParse(AccountBalance.Text, out input))
            {
                AccountBalance.Background = Brushes.LightCoral;
                return false;
            }
            else
            {
                AccountBalance.Background = Brushes.White;
                return true;
            }
        }
    }
}
