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

        Random rand;

        Repository repository;
        Bank bank;

        public MainWindow()
        {
            InitializeComponent();

            rand = new Random();

            mainPage = new MainPage(this);
            newCustomerPage = new NewCustomerPage(this);
            customerAccountPage = new CustomerAccountPage(this);

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

            MainFrame.Content = customerAccountPage;
            customerAccountPage.CustomerName.Text = customer.Name;

            CheckDepositAccount(customer);
            CheckNotDepositAccount(customer);
        }

        public void CheckTransfer(int sender,int recipient, long sum)
        {
            CustomersAccount senderAccount = bank.FindAccountByNumber(sender);
            CustomersAccount recipientAccount = bank.FindAccountByNumber(recipient);

            Customers senderCustomer = bank.FindCustomerByID(senderAccount.ID);
            Customers recipientCustomer = bank.FindCustomerByID(recipientAccount.ID);

            if(senderCustomer != null && recipientCustomer != null)
            {
                if(bank.Send(senderAccount, sum))
                {
                    bank.Receive(recipientAccount, sum);
                }
            }
            else
            {
                MessageBox.Show("Такой счёт не зарегистрирован");
            }

            MainPage();
        }

        public void AddNewCustomer(string[] customer,int accountNumber, long inputBalance,bool accountType)
        {
            Customers newCustomer = new Customers(NewCustomerId(), customer[1], customer[2], customer[3]);
            CustomersAccount newAccount = new CustomersAccount(newCustomer, accountNumber, inputBalance,accountType);

            customer[0] = newCustomer.ID.ToString();
            string line = string.Join("#", customer);

            repository.WriteCustomersAccount(newAccount);
            repository.FileWriting(repository.CustomersPath, line);

            bank.RefreshAccountsArray();
            bank.CustomersList.Add(newCustomer);
        }

        public void AddNewAccount(int id, long balance, bool accountType)
        {
            Customers customer = bank.FindCustomerByID(id);
            CustomersAccount newAccount = new CustomersAccount(customer, CreateAccountNumber(),balance,accountType);

            repository.WriteCustomersAccount(newAccount);
            bank.RefreshAccountsArray();
        }

        public int CreateAccountNumber()
        {
            while (true)
            {
                int accountNumber = rand.Next(100000, 1000000);

                if (UniqueAccountNumber(accountNumber))
                {
                    return accountNumber;
                }
            }
        }

        public void DeleteCustomer()
        {
            Customers customer = mainPage.CustomersView.SelectedItem as Customers;
            CustomersAccount customerDepositAccount = bank.FindAccountByID(customer.ID, true);
            CustomersAccount customerNotDepositAccount = bank.FindAccountByID(customer.ID, false);

            bank.CustomersList.Remove(customer);

            DeleteAccount(customerDepositAccount);
            DeleteAccount(customerNotDepositAccount);

            repository.DeleteFile(repository.CustomersPath);

            for (int i = 0; i < bank.CustomersList.Count; i++)
            {
                CustomersAccount depositAccount = bank.FindAccountByID(bank.CustomersList[i].ID, true);
                CustomersAccount notDepositAccount = bank.FindAccountByID(bank.CustomersList[i].ID, false);

                bank.CustomersList[i].ID = i;

                string line = string.Join("#", bank.CustomersList[i].ID, 
                                               bank.CustomersList[i].LastName, 
                                               bank.CustomersList[i].FirstName, 
                                               bank.CustomersList[i].MiddleName);
                repository.FileWriting(repository.CustomersPath, line);
            }

            mainPage.CustomersView.Items.Refresh();

            bank.accounts = repository.GetAccountArray();

            for (int i = 0;i < bank.accounts.Length; i++)
            {
                repository.WriteCustomersAccount(bank.accounts[i]);
            }
        }

        private void DeleteAccount(CustomersAccount customersAccount)
        {
            if(customersAccount == null) return;
            else
            {
                string AccountPath = repository.AccountPath + customersAccount.AccountNumber.ToString() + ".txt";
                repository.DeleteFile(AccountPath);
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
                if(accountNumber == bank.accounts[i].AccountNumber)
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

        private void CheckDepositAccount(Customers customer)
        {
            CustomersAccount DepositAccount = bank.FindAccountByID(customer.ID, true);

            if(DepositAccount != null)
            {
                customerAccountPage.DepositFrame.Content = customerAccountPage.depositAccountPage;

                customerAccountPage.depositAccountPage.AccountNumber.Text = DepositAccount.AccountNumber.ToString();
                customerAccountPage.depositAccountPage.AccountBalance.Text = DepositAccount.AccountBalance.ToString();

                customerAccountPage.depositAccountPage.NextMonth.Text = bank.MonthlyInterest(DepositAccount.AccountType,DepositAccount.AccountBalance).ToString();
                customerAccountPage.depositAccountPage.NextYear.Text = bank.YearIntetest(DepositAccount.AccountType, DepositAccount.AccountBalance).ToString();

                customerAccountPage.depositAccountPage._semderNumber = DepositAccount.AccountNumber;
                customerAccountPage.depositAccountPage._accountType = DepositAccount.AccountType;
            }
            else
            {
                customerAccountPage.DepositFrame.Content = customerAccountPage.newDepositAccountPage;
                customerAccountPage.newDepositAccountPage._accountType = true;
                customerAccountPage.newDepositAccountPage._customerId = customer.ID;
            }
            
        }

        private void CheckNotDepositAccount(Customers customer)
        {
            CustomersAccount NotDepositAccount = bank.FindAccountByID(customer.ID,false);

            if (NotDepositAccount != null)
            {
                customerAccountPage.NotDepositFrame.Content = customerAccountPage.notDepositAccountPage;

                customerAccountPage.notDepositAccountPage.AccountNumber.Text = NotDepositAccount.AccountNumber.ToString();
                customerAccountPage.notDepositAccountPage.AccountBalance.Text = NotDepositAccount.AccountBalance.ToString();

                customerAccountPage.notDepositAccountPage.NextMonth.Text = bank.MonthlyInterest(NotDepositAccount.AccountType,NotDepositAccount.AccountBalance).ToString();
                customerAccountPage.notDepositAccountPage.NextYear.Text = bank.YearIntetest(NotDepositAccount.AccountType, NotDepositAccount.AccountBalance).ToString();

                customerAccountPage.notDepositAccountPage._semderNumber = NotDepositAccount.AccountNumber;
                customerAccountPage.notDepositAccountPage._accountType = NotDepositAccount.AccountType;
            }
            else
            {
                customerAccountPage.NotDepositFrame.Content = customerAccountPage.newNotDepositAccountPage;
                customerAccountPage.newNotDepositAccountPage._accountType = false;
                customerAccountPage.newNotDepositAccountPage._customerId = customer.ID;
            }
        }
    }
}
