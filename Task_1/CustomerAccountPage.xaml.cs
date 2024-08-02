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
        TransferPage transferPage;
        public CustomerAccountPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            transferPage = new TransferPage(mainWindow);
        }

        private void TransferPage(object sender, RoutedEventArgs e)
        {
            this.CustomerFrame.Content = transferPage;
        }

        private void MainMenuPage(object sender, RoutedEventArgs e)
        {
            mainWindow.MainPage();
        }
    }
}
