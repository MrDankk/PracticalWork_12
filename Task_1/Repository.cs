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
        public string AccountPath { get { return accountPath; } }

        public Repository()
        {
            customersPath = "Customers.txt";
            accountPath = "Accounts\\";

            FileChecking(customersPath);
            FolderChecking(accountPath);
        }

        public Customers[] GetCustomersArray()
        {
            List<string> customersFile = FileReader(customersPath);
            Customers[] customers = new Customers[customersFile.Count];

            for (int i = 0; i < customersFile.Count; i++)
            {
                string[] customersString = customersFile[i].Split('#');

                customers[i] = new Customers(int.Parse(customersString[0]), customersString[1], customersString[2], customersString[3]);
            }

            return customers;
        }

        public CustomersAccount[] GetAccountArray()
        {
            string[] files = Directory.GetFiles(accountPath);
            CustomersAccount[] accounts = new CustomersAccount[files.Length];

            for (int i = 0; i < files.Length; i++)
            {
                List<string> strings =FileReader(files[i]);
                string[] fileSplit = strings[0].Split('#');

                bool accountType;

                if (fileSplit[3] == "0")
                {
                    accountType = false;
                }
                else
                {
                    accountType = true;
                }

                accounts[i] = new CustomersAccount(int.Parse(fileSplit[0]), int.Parse(fileSplit[1]), long.Parse(fileSplit[2]), accountType);

            }

            return accounts;
        }

        public long GetAccountBalance(CustomersAccount customerAccount)
        {
            string path = accountPath + customerAccount.AccountNumber.ToString() + ".txt";
            List<string> file = FileReader(path);
            string[] fileArray = file[0].Split('#');

            long accountBalance = long.Parse(fileArray[2]);
            return accountBalance;
        }

        public void SetAccountBalance(CustomersAccount customerAccount, long balance)
        {
            string path = accountPath + customerAccount.AccountNumber.ToString() + ".txt";

            string newAccountText = customerAccount.ID.ToString() + "#" +
                                customerAccount.AccountNumber.ToString() + "#" +
                                balance.ToString() + "#" +
                                StringAccountType(customerAccount);


            if (File.Exists(path))
            {
                File.Delete(path);
            }

            FileWriting(path, newAccountText);
        }

        public void WriteCustomersAccount(CustomersAccount customerAccount)
        {
            string path = accountPath + customerAccount.AccountNumber.ToString() + ".txt";

            string accountType = StringAccountType(customerAccount);

            string newAccount = customerAccount.ID.ToString() + "#" +
                                customerAccount.AccountNumber.ToString() + "#" +
                                customerAccount.AccountBalance.ToString() + "#" +
                                accountType;


            if (File.Exists(path))
            {
                File.Delete(path);
            }

            FileWriting(path, newAccount);
        }

        private string StringAccountType(CustomersAccount customerAccount)
        {
            string accountType;

            if (customerAccount.AccountType)
            {
                accountType = "1";
            }
            else
            {
                accountType = "0";
            }

            return accountType;
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

        private void FolderChecking(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        public void DeleteFile(string filePath)
        {
            File.Delete(filePath);
        }

        #endregion
    }
}
