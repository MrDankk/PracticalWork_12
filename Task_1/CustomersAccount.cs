using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    internal class CustomersAccount
    {
        private int id;
        private int accountNumber;
        private long accoundBalance;
        private bool accountType;

        public long AccountBalance
        {
            get { return accoundBalance; }
            set { accoundBalance = value; }
        }

        public int AccountNumber { get { return accountNumber; } }

        public bool AccountType { get { return accountType; } }

        public int ID { get { return id; } }

        public CustomersAccount (Customers customer, int accountNumber, long accountBalance,bool accountType)
        {
            this.id = customer.ID;
            this.accountNumber = accountNumber;
            this.accoundBalance = accountBalance;
            this.accountType = accountType;
        }

        public CustomersAccount(int id, int accountNumber, long accoundBalance, bool accountType)
        {
            this.id = id;
            this.accountNumber = accountNumber;
            this.accoundBalance = accoundBalance;
            this.accountType = accountType;
        }

        public CustomersAccount(CustomersAccount customersAccount)
        {
            this.id = customersAccount.ID;
            this.accountNumber = customersAccount.AccountNumber;
            this.accoundBalance = customersAccount.AccountBalance;
            this.accountType = customersAccount.AccountType;
        }
    }
}
