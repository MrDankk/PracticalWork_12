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
    /// Логика взаимодействия для TransferPage.xaml
    /// </summary>
    public partial class TransferPage : Page
    {
        MainWindow mainWindow;

        public TransferPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void AccountInfoPage(object sender, RoutedEventArgs e)
        {
            mainWindow.customerAccountPage.CustomerFrame.Content = mainWindow.accountInfo;
        }

        private void OpenTransfer(object sender, RoutedEventArgs e)
        {
            if(CheckInput())
            {
                int senderNumber = int.Parse(mainWindow.accountInfo.AccountNumber.Text);
                int recipientNumber = int.Parse(AccountNumber.Text);
                long sum = long.Parse(Sum.Text);

                mainWindow.CheckTransfer(senderNumber, recipientNumber, sum);
            }
        }

        private bool CheckInput()
        {
            bool result = true;
            int checkInt;
            long checkLong;

            if (AccountNumber.Text.Trim() == "" && !Int32.TryParse(AccountNumber.Text, out checkInt))
            {
                AccountNumber.Background = Brushes.LightCoral;
                result = false;
            }
            else
            {
                AccountNumber.Background = Brushes.White;
            }

            if(Sum.Text.Trim() == "" && !Int64.TryParse(Sum.Text.Trim(), out checkLong))
            {
                Sum.Background = Brushes.LightCoral;
                result = false;
            }
            else
            {
                Sum.Background= Brushes.White;
            }

            return result;
        }

    }
}
