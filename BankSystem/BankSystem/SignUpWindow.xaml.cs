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
using System.Windows.Shapes;

namespace BankSystem
{
    /// <summary>
    /// Interaction logic for VerifyAccount.xaml
    /// </summary>
    public partial class SignUPWindow : Window
    {
        private SmsService smsService = new SmsService();
        private readonly AuthManager authManager;
        private bool isVerified = false;
        private int code;

        public SignUPWindow(AuthManager manager)
        {
            InitializeComponent();
            authManager = manager;
        }

        private void SignUpBtnClick(object sender, RoutedEventArgs e)
        {
            if (!IsOk())
            {
                MessageBox.Show("Заполните все поля нормально!");
                return;
            }

            var currentAccount = authManager.SignUp(fullNameTB.Text, mobileNumberTB.Text, passwordTB.Text);
            if (currentAccount is null)
            {
                MessageBox.Show("Некорректные данные!");
                return;
            }
            var homeWindow = new HomeWindow();
            homeWindow.Show();
            Close();
        }

        private void SendCodeBtnClick(object sender, RoutedEventArgs e)
        {
            if (!IsOk())
            {
                MessageBox.Show("Заполните все поля!");
            }
            code = new Random().Next(1000, 10000);
            var message = $"Ваш код подтверждения: {code}";
            if (!smsService.SendMessage(mobileNumberTB.Text, message))
            {
                MessageBox.Show("Произошла ошибка, попытайтесь чуточку позже!");
            }
            sendCodeBtn.Visibility = Visibility.Hidden;
            verifyCodeBtn.Visibility = Visibility.Visible;
        }

        private void VerifyCodeBtnClick(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(codeTb.Text, out var userCode) || userCode != code)
            {
                MessageBox.Show("Неправильный код!");
                return;
            }
            isVerified = true;
            MessageBox.Show("Вы успешно подтвердили ваш номер, нажмите кнопку регистрации!");
        }

        private bool IsOk()
        {
            if (string.IsNullOrEmpty(fullNameTB.Text) || string.IsNullOrEmpty(mobileNumberTB.Text) || string.IsNullOrEmpty(passwordTB.Text) || passwordTB.Text.Length < 6 || mobileNumberTB.Text[0] != '+' || mobileNumberTB.Text.Length != 12 || !isVerified)
                return false;
            return true;
        }
    }
}
