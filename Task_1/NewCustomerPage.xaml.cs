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
    /// Логика взаимодействия для NewCustomerPage.xaml
    /// </summary>
    public partial class NewCustomerPage : Page
    {
        MainWindow mainWindow;

        Random random;

        bool _accountTupe;
        long deposit;

        public NewCustomerPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            random = new Random();
        }

        private void ReturnMainPage(object sender, RoutedEventArgs e)
        {
            Reset();
            mainWindow.MainPage();
        }

        private void CreateNewCustomer(object sender, RoutedEventArgs e)
        {
            if(CheckEmptyInput())
            {
                string[] customer = new string[] {"0", LastName.Text, FirstName.Text, MiddleName.Text, CreateAccountNumber().ToString()};
                mainWindow.AddNewCustomer(customer, Deposit.Text, _accountTupe);

                Reset();
                mainWindow.MainPage();
            }
        }

        private int CreateAccountNumber()
        {
            while(true)
            {
                int accountNumber = random.Next(100000, 1000000);

                if (mainWindow.UniqueAccountNumber(accountNumber))
                {
                    return accountNumber;
                }
            }
        }

        private bool CheckEmptyInput()
        {
            bool result = true;

            if (LastName.Text.Trim() == "")
            {
                LastName.Background = Brushes.LightCoral;
                result = false;
            }
            else
            {
                LastName.Background = Brushes.White;
            }

            if (FirstName.Text.Trim() == "")
            {
                FirstName.Background = Brushes.LightCoral;
                result = false;
            }
            else
            {
                FirstName.Background = Brushes.White;
            }

            if (MiddleName.Text.Trim() == "")
            {
                MiddleName.Background = Brushes.LightCoral;
                result = false;
            }
            else
            {
                MiddleName.Background = Brushes.White;
            }

            if (Int64.TryParse(Deposit.Text.Trim(), out deposit))
            {
                Deposit.Background = Brushes.White;
            }
            else
            {
                Deposit.Background = Brushes.LightCoral;
                result = false;
            }

            if(DepositRadioButton.IsChecked == false && NotDepositRadioButton.IsChecked == false)
            {
                result = false;
                DepositRadioButton.Background = Brushes.LightCoral;
                NotDepositRadioButton.Background = Brushes.LightCoral;
            }
            else
            {
                DepositRadioButton.Background = Brushes.White;
                NotDepositRadioButton.Background = Brushes.White;
            }

            return result;
        }

        private void Reset()
        {
            LastName.Text = string.Empty;
            FirstName.Text = string.Empty;
            MiddleName.Text = string.Empty;
            Deposit.Text = string.Empty;
            DepositRadioButton.IsChecked = false;
            NotDepositRadioButton.IsChecked = false;
        }

        private void DepositAccountType(object sender, RoutedEventArgs e)
        {
            _accountTupe = true;
        }

        private void NotDepositAccountType(object sender, RoutedEventArgs e)
        {
            _accountTupe = false;
        }
    }
}
