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
    /// Логика взаимодействия для CustomerAccountPage.xaml
    /// </summary>
    public partial class CustomerAccountPage : Page
    {
        MainWindow mainWindow;

        public AccountPage depositAccountPage;
        public AccountPage notDepositAccountPage;

        public NewAccountPage newDepositAccountPage;
        public NewAccountPage newNotDepositAccountPage;

        public TransferPage depositTransferPage;
        public TransferPage notDepositTransferPage;

        public CustomerAccountPage(MainWindow mainWindow)
        {
            InitializeComponent(); 
            this.mainWindow = mainWindow;

            depositAccountPage = new AccountPage(this);
            notDepositAccountPage = new AccountPage(this);

            newDepositAccountPage = new NewAccountPage(mainWindow);
            newNotDepositAccountPage = new NewAccountPage(mainWindow);

            depositTransferPage = new TransferPage(mainWindow);
            notDepositTransferPage = new TransferPage(mainWindow);

            newDepositAccountPage.AccountType.Text = "Депозитный счёт";
            depositAccountPage.AccountType.Text = "Депозитный счёт";

            newNotDepositAccountPage.AccountType.Text = "Недепозитный счёт";
            notDepositAccountPage.AccountType.Text = "Недепозитный счёт";
        }

        private void MainMenuPage(object sender, RoutedEventArgs e)
        {
            mainWindow.MainPage();
        }
    }
}
