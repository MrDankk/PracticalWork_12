using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Task_1
{
    internal class Bank : ISending<CustomersAccount>, IReceiving<CustomersAccount>
    {
        private Repository repository;

        public CustomersAccount[] accounts;
        public ObservableCollection<Customers> CustomersList;

        public Bank(Repository repository)
        {
            this.repository = repository;
            RefreshAccountsArray();

            CustomersList = new ObservableCollection<Customers>();

            Customers[] customers = repository.GetCustomersArray();

            for (int i = 0; i < customers.Length; i++)
            {
                CustomersList.Add(customers[i]);
            }
        }

        /// <summary>
        /// Начисление процентов за месяц
        /// </summary>
        /// <param name="accountType"> Тип счёта </param>
        /// <param name="balance"> Баланс </param>
        /// <returns></returns>
        public long MonthlyInterest(bool accountType, long balance)
        {
            long month = balance;

            if (accountType)
            {
                float percentages = (float)(balance * 1.12) / 12;
                month += (long)percentages;
            }

            return month;
        }

        /// <summary>
        /// Начисление процентов за год
        /// </summary>
        /// <param name="accountType"> Тип счёта </param>
        /// <param name="balance"> Баланс </param>
        /// <returns></returns>
        public long YearIntetest(bool accountType, long balance)
        {
            long year = balance;

            if (accountType)
            {
                for (int i = 0; i < 12; i++)
                {
                    float percentages = (float)(year * 1.12) / 12;
                    year += (long)percentages;
                }
            }
            else
            {
                float percentages = (float)(year * 0.12);
                year += (long)percentages;
            }

            return year;
        }

        public void RefreshAccountsArray()
        {
            accounts = repository.GetAccountArray();
        }

        public bool Send(CustomersAccount senderAccount,long sum)
        {
            long newBalance;

            if (BalanceChecking(senderAccount, sum))
            {
                newBalance = senderAccount.AccountBalance -= sum;

                senderAccount.AccountBalance = newBalance;
                repository.SetAccountBalance(senderAccount, newBalance);
                return true;
            }
            else
            {
                MessageBox.Show("Не хватает баланса");
                return false;
            }
        }

        public void Receive(CustomersAccount recipientAccount, long sum)
        {
            long newBalance = recipientAccount.AccountBalance += sum;

            recipientAccount.AccountBalance = newBalance;
            repository.SetAccountBalance(recipientAccount, newBalance);
        }

        /// <summary>
        /// Проверка баланса
        /// </summary>
        /// <param name="sender"> Счёт отправителя </param>
        /// <param name="sum"> Сумма </param>
        /// <returns></returns>
        private bool BalanceChecking(CustomersAccount sender, long sum)
        {
            if (sender.AccountBalance - sum >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Поиск клиента
        /// </summary>
        /// <param name="_accountNumber"> Номер счёта </param>
        /// <returns></returns>
        public Customers FindCustomerByID(int _id)
        {
            for (int i = 0; i < CustomersList.Count; i++)
            {
                if (CustomersList[i].ID == _id)
                {
                    return CustomersList[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Поиск счёта
        /// </summary>
        /// <param name="_accountNumber"> Номер счёта </param>
        /// <returns></returns>
        public CustomersAccount FindAccountByID(int _id, bool accountType)
        {
            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i].ID == _id && accounts[i].AccountType == accountType)
                {
                    return accounts[i];
                }
            }

            return null;
        }

        public CustomersAccount FindAccountByNumber(int accountNumber)
        {
            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i].AccountNumber == accountNumber)
                {
                    return accounts[i];
                }
            }

            return null;
        }

        public int FindIDByAccountNumber(int _accountNumber)
        {
            int id = -1;

            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i].AccountNumber == _accountNumber)
                {
                    id = accounts[i].ID;
                }
            }
            return id;
        }
    }
}
