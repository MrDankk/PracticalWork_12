using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainPage mainPage;
        NewCustomerPage newCustomerPage;
        public CustomerAccountPage customerAccountPage;
        public AccountInfo accountInfo;

        Repository repository;
        Bank bank;

        public MainWindow()
        {
            InitializeComponent();

            mainPage = new MainPage(this);
            newCustomerPage = new NewCustomerPage(this);
            customerAccountPage = new CustomerAccountPage(this);
            accountInfo = new AccountInfo(this);

            repository = new Repository();
            bank = new Bank(repository);

            mainPage.CustomersView.ItemsSource = bank.CustomersList;

            MainPage();
        }

        public void MainPage()
        {
            MainFrame.Content = mainPage;
        }

        public void NewCustomerPage()
        {
            MainFrame.Content = newCustomerPage;
        }

        public void CustomerAccountPage()
        {
            Customers customer = mainPage.CustomersView.SelectedItem as Customers;
            CustomersAccount customerAccount = new CustomersAccount(customer, repository);

            MainFrame.Content = customerAccountPage;
            customerAccountPage.CustomerFrame.Content = accountInfo;

            accountInfo.CustomerName.Text = customer.Name;
            accountInfo.AccountNumber.Text = customer.AccountNumber.ToString();
            accountInfo.AccountBalance.Text = customerAccount.AccountBalance.ToString();
            accountInfo.AccountTupe.Text = CustomerAccountType(customerAccount);
            accountInfo.NextMonth.Text = bank.MonthlyInterest(customerAccount.AccountType, customerAccount.AccountBalance).ToString();
            accountInfo.NextYear.Text = bank.YearIntetest(customerAccount.AccountType, customerAccount.AccountBalance).ToString();
        }

        public void CheckTransfer(int sender,int recipient, long sum)
        {
            bank.Transfer(sender, recipient, sum);
            MainPage();
        }

        public void AddNewCustomer(string[] customer, string inputBalance,bool accountType)
        {
            Customers newCustomer = new Customers(NewCustomerId(), customer[1], customer[2], customer[3], int.Parse(customer[4]));
            customer[0] = newCustomer.ID.ToString();
            string line = string.Join("#", customer);

            long balance = long.Parse(inputBalance);
            repository.WriteCustomersAccount(newCustomer, accountType, balance);

            bank.CustomersList.Add(newCustomer);
            repository.FileWriting(repository.CustomersPath, line);
        }

        public void DeleteCustomer()
        {
            Customers customer = mainPage.CustomersView.SelectedItem as Customers;
            CustomersAccount customerAccount = bank.FindAccountByAccountNumber(customer.AccountNumber);

            bank.CustomersList.Remove(customer);

            File.Delete(repository.CustomersPath);
            for (int i = 0; i < bank.CustomersList.Count; i++)
            {
                bank.CustomersList[i].ID = i;

                string line = string.Join("#", bank.CustomersList[i].ID, 
                                               bank.CustomersList[i].LastName, 
                                               bank.CustomersList[i].FirstName, 
                                               bank.CustomersList[i].MiddleName, 
                                               bank.CustomersList[i].AccountNumber);
                repository.FileWriting(repository.CustomersPath, line);
            }

            mainPage.CustomersView.Items.Refresh();

            bank.accounts = new CustomersAccount[bank.accounts.Length - 1];
            for (int i = 0;i < bank.accounts.Length; i++)
            {
                bank.accounts[i] = new CustomersAccount(bank.CustomersList[i], repository);

                repository.WriteCustomersAccount(bank.CustomersList[i],bank.accounts[i].AccountType, bank.accounts[i].AccountBalance);
            }
        }

        /// <summary>
        /// Проверка уникальности номера счёта
        /// </summary>
        /// <param name="accountNumber"> номер счета </param>
        /// <returns></returns>
        public bool UniqueAccountNumber(int accountNumber)
        {
            for (int i = 0; i < bank.CustomersList.Count;i++)
            {
                if(accountNumber == bank.CustomersList[i].AccountNumber)
                {
                    return false;
                }
            }

            return true;
        }

        private int NewCustomerId()
        {
            return bank.CustomersList.Count;
        }

        private string CustomerAccountType(CustomersAccount customerAccount)
        {
            string account = string.Empty;

            if (customerAccount.AccountType)
            {
                account = "Депозитный счёт";
            }
            else
            {
                account = "Недепозитный счёт";
            }

            return account;
        }
    }
}
