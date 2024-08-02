using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Task_1
{
    class Repository
    {
        private string customersPath;
        private string accountPath;

        public string CustomersPath { get { return customersPath; } }

        public Repository()
        {
            customersPath = "Customers.txt";
            accountPath = "Accounts\\";
        }

        public Customers[] GetCustomersArray()
        {
            List<string> customersFile = FileReader(customersPath);
            Customers[] customers = new Customers[customersFile.Count];

            for (int i = 0; i < customersFile.Count; i++)
            {
                string[] customersString = customersFile[i].Split('#');

                customers[i] = new Customers(int.Parse(customersString[0]), customersString[1], customersString[2], customersString[3], int.Parse(customersString[4]));
            }

            return customers;
        }

        public CustomersAccount[] GetAccountArray()
        {
            Customers[] customers = GetCustomersArray();
            CustomersAccount[] accounts = new CustomersAccount[customers.Length];

            for (int i = 0; i < accounts.Length; i++)
            {
                accounts[i] = new CustomersAccount(customers[i], this);
            }

            return accounts;
        }

        public long GetAccountBalance(Customers customer)
        {
            string path = accountPath + customer.AccountNumber.ToString() + ".txt";
            List<string> file = FileReader(path);
            string[] fileArray = file[0].Split('#');

            long accountBalance = long.Parse(fileArray[0]);
            return accountBalance;
        }

        public void SetAccountBalance(Customers customer, long balance)
        {
            string path = accountPath + customer.AccountNumber.ToString() + ".txt";
            string newBalance = string.Empty;
            bool accountType = GetAccountType(customer);

            if(accountType)
            {
                newBalance = balance.ToString() + "#" + "1";
            }
            else
            {
                newBalance = balance.ToString() + "#" + "0";
            }

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            FileWriting(path, balance.ToString());
        }

        public bool GetAccountType(Customers customer)
        {
            string path = accountPath + customer.AccountNumber.ToString() + ".txt";
            List<string> file = FileReader(path);
            string[] fileArray = file[0].Split('#');

            bool accountType = true;

            if (fileArray[1] == "0")
            {
                accountType = false;
            }

            return accountType;
        }
        public void WriteCustomersAccount(Customers customer, bool accountType, long balance)
        {
            string path = accountPath + customer.AccountNumber.ToString() + ".txt";
            string newAccount = string.Empty;

            if (accountType)
            {
                newAccount = balance.ToString() + "#" + "1";
            }
            else
            {
                newAccount = balance.ToString() + "#" + "0";
            }

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            FileWriting(path, newAccount);
        }

        #region Проверка,чтение и запись файлов

        /// <summary>
        /// Чтение файла
        /// </summary>
        /// <param name="filePath"> Путь к файлу </param>
        /// <returns> Лист строк файла </returns>
        private List<string> FileReader(string filePath)
        {
            FileChecking(filePath);

            List<string> file = new List<string>();

            using (StreamReader streamReader = new StreamReader(filePath))
            {
                string line;

                while ((line = streamReader.ReadLine()) != null)
                {
                    file.Add(line);
                }
            }

            return file;
        }

        /// <summary>
        /// Запись файла
        /// </summary>
        /// <param name="filePath"> Путь к файлу </param>
        /// <param name="recording"> Что необходимо записать </param>
        public void FileWriting(string filePath, string recording)
        {
            FileChecking(filePath);

            using (StreamWriter streamWriter = new StreamWriter(filePath, true))
            {
                if (recording.Trim() != "")
                {
                    streamWriter.WriteLine(recording);
                }
            }
        }

        /// <summary>
        /// Проверка файла
        /// </summary>
        /// <param name="filePath"> Путь к файлу </param>
        private void FileChecking(string filePath)
        {
            if (!File.Exists(filePath))
            {
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                fileStream.Close();
            }
        }

        #endregion
    }
}
