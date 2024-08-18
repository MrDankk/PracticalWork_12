using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    internal class Customers
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Name { get; set; }

        public Customers(int id,string lastName,string firstName,string middleName) 
        { 
            this.ID = id;
            this.LastName = lastName;
            this.FirstName = firstName;
            this.MiddleName = middleName;
            this.Name = lastName + " " + firstName + " " + middleName;
        }

        public Customers(Customers customer) 
        {
            this.ID = customer.ID;
            this.LastName = customer.LastName;
            this.FirstName = customer.FirstName;
            this.MiddleName = customer.MiddleName;
            this.Name = customer.Name;
        }
    }
}
