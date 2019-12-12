using BankSystem.Services;
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

namespace BankSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AuthManager authManager = new AuthManager();
        private SmsService smsService = new SmsService();
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SingInBtnClick(object sender, RoutedEventArgs e)
        {
            var currentAccount = authManager.SignIn(mobileNumberTB.Text, passwordTB.Text);
            if (currentAccount is null)
            {
                MessageBox.Show("Некорректные данные!");
                return;
            }
            var homeWindow = new HomeWindow();
            homeWindow.Show();
            Close();
        }

        private void ShowSignUpBtnClick(object sender, RoutedEventArgs e)
        {
            var signUpWindow = new SignUPWindow(authManager);
            signUpWindow.Show();
            Close();
        }


    }
}
