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
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        private Account account;
        private SmsService smsService = new SmsService();
        private Func<string, string, bool> action;

        public HomeWindow(Account currentAccount)
        {
            account = currentAccount;
            InitializeComponent();
            ShowInfo();
            action = new Func<string, string, bool>(smsService.SendMessage);
        }

        private void ShowInfo()
        {
            mNumberL.Content = account.MobileNumber;
            fullNameL.Content = account.FullName;
            balanceL.Content = account.Balance;
        }

        private void WithdrawBtnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(sumTB.Text))
            {
                MessageBox.Show("Пустое поле!");
                return;
            }
            if(!double.TryParse(sumTB.Text, out var sum))
            {
                MessageBox.Show("Неправильно введено число");
                return;
            }
            if (sum > account.Balance)
            {
                MessageBox.Show("Не хватает денег!");
                return;
            }
            using(var context = new BankSystemContext())
            {
                account.Balance -= sum;
                context.SaveChanges();
            }
            var message = $"Вам на счет поступило {sum} тенге";
            action.BeginInvoke(account.MobileNumber, message, ProcessResult, null);
        }

        private void ReplenishBtnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(sumTB.Text))
            {
                MessageBox.Show("Пустое поле!");
                return;
            }
            if (!double.TryParse(sumTB.Text, out var sum))
            {
                MessageBox.Show("Неправильно введено число");
                return;
            }
            using (var context = new BankSystemContext())
            {
                account.Balance += sum;
                context.SaveChanges();
            }
            var message = $"С вашего счета сняли {sum} тенге";
            action.BeginInvoke(account.MobileNumber, message, ProcessResult, null);
        }

        private void ProcessResult(IAsyncResult result)
        {
            var resultProcess = action.EndInvoke(result);
            if (resultProcess) MessageBox.Show("Операция успешна завершена");
            else MessageBox.Show("Что-то пошло не так!");
        }
    }
}
