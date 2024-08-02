using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    internal class CustomersAccount
    {
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

        public CustomersAccount (Customers customer, Repository repository)
        { 
            this.accountNumber = customer.AccountNumber;
            this.accoundBalance = repository.GetAccountBalance(customer);
            this.accountType = repository.GetAccountType(customer);
        }

        public CustomersAccount(CustomersAccount customersAccount)
        {
            this.accountNumber = customersAccount.AccountNumber;
            this.accoundBalance = customersAccount.AccountBalance;
            this.accountType = customersAccount.AccountType;
        }
    }
}
