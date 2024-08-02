using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Task_1
{
    internal class Bank
    {
        private Repository repository;

        public CustomersAccount[] accounts;
        public ObservableCollection<Customers> CustomersList;

        public Bank(Repository repository)
        {
            this.repository = repository;

            accounts = repository.GetAccountArray();

            CustomersList = new ObservableCollection<Customers>();

            Customers[] customers = repository.GetCustomersArray();

            for (int i = 0; i < customers.Length; i++)
            {
                CustomersList.Add(customers[i]);
            }
        }

        /// <summary>
        /// Перевод между счетами
        /// </summary>
        /// <param name="sender"> Номер счёта отправителя </param>
        /// <param name="recipient"> Номер счёта  получателя </param>
        /// <param name="sum"> Сумма </param>
        public void Transfer(int sender, int recipient, long sum)
        {
            CustomersAccount senderAccount = FindAccountByAccountNumber(sender);
            CustomersAccount recipientAccount = FindAccountByAccountNumber(recipient);

            Customers senderCustomer = FindCustomerByAccountNumber(sender);
            Customers recipientCustomer = FindCustomerByAccountNumber(recipient);

            if(recipientAccount != null && recipientCustomer != null)
            {
                if (BalanceChecking(senderAccount, sum))
                {
                    Send(senderCustomer, senderAccount, sum);
                    Receive(recipientCustomer, recipientAccount, sum);
                }
                else
                {
                    MessageBox.Show("Не хватает баланса");
                }
            }
            else
            {
                MessageBox.Show("Такой счёт не зарегистрирован");
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
                float percentages = (float)(year * 1.12);
                year += (long)percentages;
            }

            return year;
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
        /// Снятие денег со счёта
        /// </summary>
        /// <param name="sender"> Отправитель </param>
        /// <param name="senderAccount"> Счёт отправителя </param>
        /// <param name="sum"> Сумма </param>
        private void Send(Customers sender, CustomersAccount senderAccount, long sum)
        {
            long newBalance = senderAccount.AccountBalance -= sum;

            senderAccount.AccountBalance = newBalance;
            repository.SetAccountBalance(sender,newBalance);
        }

        /// <summary>
        /// Зачисление денег на счёт
        /// </summary>
        /// <param name="recipient"> Получатель </param>
        /// <param name="recipientAccount"> Счёт получателя </param>
        /// <param name="sum"> Сумма </param>
        private void Receive(Customers recipient, CustomersAccount recipientAccount, long sum)
        {
            long newBalance = recipientAccount.AccountBalance += sum;

            recipientAccount.AccountBalance = newBalance;
            repository.SetAccountBalance(recipient,newBalance);
        }

        /// <summary>
        /// Поиск клиента
        /// </summary>
        /// <param name="_accountNumber"> Номер счёта </param>
        /// <returns></returns>
        public Customers FindCustomerByAccountNumber(int _accountNumber)
        {
            for (int i = 0; i < CustomersList.Count; i++)
            {
                if (CustomersList[i].AccountNumber == _accountNumber)
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
        public CustomersAccount FindAccountByAccountNumber(int _accountNumber)
        {
            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i].AccountNumber == _accountNumber)
                {
                    return accounts[i];
                }
            }

            return null;
        }
    }
}
