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
    /// Логика взаимодействия для AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        CustomerAccountPage customerAccountPage;

        public bool _accountType;
        public int _semderNumber;

        public AccountPage(CustomerAccountPage customerAccountPage)
        {
            InitializeComponent();
            this.customerAccountPage = customerAccountPage;
        }

        private void OpenTransferPage(object sender, RoutedEventArgs e)
        {
            if (_accountType)
            {
                customerAccountPage.DepositFrame.Content = customerAccountPage.depositTransferPage;
                customerAccountPage.depositTransferPage.senderNumber = _semderNumber;
            }
            else
            {
                customerAccountPage.NotDepositFrame.Content = customerAccountPage.notDepositTransferPage;
                customerAccountPage.notDepositTransferPage.senderNumber = _semderNumber;
            }
        }
    }
}
