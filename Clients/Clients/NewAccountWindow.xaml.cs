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
    /// Логика взаимодействия для NewAccountWindow.xaml
    /// </summary>
    public partial class NewAccountWindow : Window
    {
        private AccountViewModel _accountModel;
        public NewAccountWindow()
        {
            _accountModel = new AccountViewModel();
            this.DataContext = _accountModel;
            InitializeComponent();
        }

        private async void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            var clientListWindow = new ListWindow(_accountModel);
            clientListWindow.Show();
            await Task.Run(() => GenerateAccountNumber(clientListWindow));
            
        }

        private void GenerateAccountNumber(ListWindow wnd)
        {
            while (wnd.Dispatcher.Invoke(() => { return wnd.IsLoaded; }))
            { }
            Random rand = new Random();
            var randomDigit = rand.Next(0, 9).ToString();
            var code = "3014";
            var clientPart = GetCorrectClientPart(_accountModel.ClientId);
            using (ClientsEntities db = new ClientsEntities())
            {
                var accountCountPart = db.GetUserAccountsCountById(_accountModel.ClientId) + 1;
                _accountModel.AccountNumber = code + clientPart + GetCorrectAccountCountPart(accountCountPart) + randomDigit;
            }
        }

        private string GetCorrectClientPart(int id)
        {
            var clientPart = id.ToString();
            while(clientPart.Length < 5)
            {
                clientPart = clientPart.Insert(0, "0");
            }
            return clientPart;
        }

        private string GetCorrectAccountCountPart(int value)
        {
            var result = value.ToString();
            while (result.Length < 3)
            {
                result = result.Insert(0, "0");
            }
            return result;
        }
    }
}
