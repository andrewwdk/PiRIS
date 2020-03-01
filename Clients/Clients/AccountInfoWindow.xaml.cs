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

namespace Clients
{
    /// <summary>
    /// Interaction logic for AccountInfoWindow.xaml
    /// </summary>
    public partial class AccountInfoWindow : Window
    {
        private AccountViewModel _account;
        public AccountInfoWindow(AccountViewModel account)
        {
            _account = account;
            this.DataContext = account;
            InitializeComponent();
            using(var db = new ClientsEntities())
            {
                var depositTypeID = db.GetDepositTypeByName(_account.DepositType).DepositTypeID;
                if((depositTypeID == 3 || depositTypeID == 4) && _account.IsPercentage == "Нет")
                {
                    PaymentsButton.IsEnabled = true;
                }
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new ClientsEntities())
            {
                var mainAcc = db.GetAccountByAccountNumber(_account.AccountNumber);
                mainAcc.IsClosed = true;
                if (mainAcc.PercentAccountID.HasValue)
                {
                    var percentAcc = db.GetAccountById(mainAcc.PercentAccountID.Value);
                    percentAcc.MoneyAmount = 0;
                    percentAcc.IsClosed = true;
                }
                db.SaveChanges();
                this.Close();
            }
        }

        private void PaymentsButton_Click(object sender, RoutedEventArgs e)
        {
            CreditPaymentsWindow creditWindow = new CreditPaymentsWindow(_account);
            creditWindow.Show();
        }
    }
}
