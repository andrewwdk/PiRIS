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
    /// Логика взаимодействия для AccountList.xaml
    /// </summary>
    public partial class AccountList : Window
    {
        public AccountList()
        {
            InitializeComponent();
            Load();
        }

        private void Load()
        {
            using (ClientsEntities db = new ClientsEntities())
            {
                var accounts = db.Account.ToList<Account>();
                var accountsModel = new List<AccountViewModel>();
                foreach(var account in accounts)
                {
                    accountsModel.Add(new AccountViewModel(account));
                }

                this.Dispatcher.Invoke(() =>
                {
                    AccountListDataGrid.ItemsSource = accountsModel.OrderBy(acc => acc.Surname)
                         .ThenBy(acc => acc.Name)
                         .ThenBy(acc => acc.Patronimic);
                });
            }
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAccount = AccountListDataGrid.SelectedItem as AccountViewModel;
            var accountInfoWindow = new AccountInfoWindow(selectedAccount);
            accountInfoWindow.Show();
        }

        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var newAccountWindow = new NewAccountWindow();
            newAccountWindow.Show();
            await Task.Run(() => RefreshList(newAccountWindow));
        }

        private void RefreshList(NewAccountWindow wnd)
        {
            while (wnd.Dispatcher.Invoke(() => { return wnd.IsLoaded; }))
            { }
            Load();
        }
    }
}
