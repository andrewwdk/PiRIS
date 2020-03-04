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
            using (var db = new ClientsEntities())
            {
                var selectedAccount = AccountListDataGrid.SelectedItem as AccountViewModel;
                var acc = db.GetAccountByAccountNumber(selectedAccount.AccountNumber);
                var accountInfoWindow = new AccountInfoWindow(new AccountViewModel(acc));
                accountInfoWindow.Show();
            }
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

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            using(var db = new ClientsEntities())
            {
                var mainAccounts = db.Account.Where(a => !a.IsClosed && a.PercentAccountID != null && (a.DepositTypeID == 1 || a.DepositTypeID == 2)).ToList();
                IncrimentDaysCount(db, mainAccounts);
                CalculatePercents(db, mainAccounts);
                EndOfMonthPayments(db, mainAccounts.Where(a => a.DepositTypeID == 1).ToList());
                EndOfTermPayments(db, mainAccounts);
                mainAccounts = db.Account.Where(a => !a.IsClosed && a.PercentAccountID != null && (a.DepositTypeID == 3 || a.DepositTypeID == 4)).ToList();
                IncrimentDaysCount(db, mainAccounts);
                EndOfMonthPayments(db, mainAccounts);
                EndOfTermPayments(db, mainAccounts);
            }
            Load();
        }

        private void IncrimentDaysCount(ClientsEntities db, List<Account> list)
        {
            foreach(var acc in list)
            {
                acc.DaysCount++;
                db.GetAccountById(acc.PercentAccountID.Value).DaysCount++;
            }
            db.SaveChanges();  
        }

        private void CalculatePercents(ClientsEntities db, List<Account> list)
        {
            foreach (var acc in list)
            {
                var percents = (db.GetDepositTypeById(acc.DepositTypeID).Percents * acc.MoneyAmount)/ (100 * 365);
                db.GetAccountById(acc.PercentAccountID.Value).MoneyAmount += Math.Round(percents, 4);
            }
            db.SaveChanges();
        }

        private void EndOfMonthPayments(ClientsEntities db, List<Account> list)
        {
            foreach (var acc in list)
            {
                if (acc.DaysCount % 30 == 0)
                {
                    var percentAccount = db.GetAccountById(acc.PercentAccountID.Value);
                    if (acc.DepositTypeID == 1)
                    {
                        acc.MoneyAmount += percentAccount.MoneyAmount;
                        var bankResources = db.BankResourse.ToList();
                        bankResources[0].RealMoney -= ConvertCurrencyToByn(acc.CurrencyID) * percentAccount.MoneyAmount;
                        percentAccount.MoneyAmount = 0;
                    }
                    
                    if(acc.DepositTypeID == 3)
                    {
                        var daysCount = (acc.EndDate - acc.StartDate).Days + 1;
                        var mainAmount = Convert.ToDouble(acc.MoneyAmount);
                        var percents = mainAmount * db.GetDepositTypeById(acc.DepositTypeID).Percents / 100;
                        var percentsByDay = Math.Round(percents / daysCount, 2);
                        var mainAmountByDay = Math.Round(mainAmount / daysCount, 2);
                        acc.MoneyAmount -= mainAmountByDay * 30;
                        percentAccount.MoneyAmount -= percentsByDay * 30;
                        var bankResources = db.BankResourse.ToList();
                        bankResources[0].RealMoney += ConvertCurrencyToByn(acc.CurrencyID) * (percentsByDay + mainAmountByDay) * 30;
                    }
                    
                    if(acc.DepositTypeID == 4)
                    {
                        var daysCount = (acc.EndDate - acc.StartDate).Days + 1;
                        var mainAmount = Convert.ToDouble(acc.MoneyAmount);
                        var percents = mainAmount * db.GetDepositTypeById(acc.DepositTypeID).Percents / 100;
                        var percentsByDay = Math.Round(percents / daysCount, 2);
                        percentAccount.MoneyAmount -= percentsByDay * 30;
                        var bankResources = db.BankResourse.ToList();
                        bankResources[0].RealMoney += ConvertCurrencyToByn(acc.CurrencyID) * percentsByDay * 30;
                    }
                }
            }
            db.SaveChanges();
        }

        private void EndOfTermPayments(ClientsEntities db, List<Account> list)
        {
            foreach (var acc in list)
            {
                if ((acc.EndDate - acc.StartDate).Days + 1 == acc.DaysCount)
                {
                    var percentAccount = db.GetAccountById(acc.PercentAccountID.Value);
                    percentAccount.IsClosed = true;
                    acc.IsClosed = true;
                    if (acc.DepositTypeID == 1 || acc.DepositTypeID == 2)
                    {
                        acc.MoneyAmount += percentAccount.MoneyAmount;
                        var bankResources = db.BankResourse.ToList();
                        bankResources[0].RealMoney -= ConvertCurrencyToByn(acc.CurrencyID) * percentAccount.MoneyAmount;
                    }
                    if (acc.DepositTypeID == 3 || acc.DepositTypeID == 4)
                    {
                        var bankResources = db.BankResourse.ToList();
                        bankResources[0].RealMoney += ConvertCurrencyToByn(acc.CurrencyID) * (percentAccount.MoneyAmount + acc.MoneyAmount);
                        acc.MoneyAmount = 0;
                    }
                    percentAccount.MoneyAmount = 0;
                }
            }
            db.SaveChanges();
        }

        private double ConvertCurrencyToByn(int id)
        {
            if (id == 2) return 2.2;
            if (id == 3) return 2.4;

            return 1;
        }

        private void BankMoneyButton_Click(object sender, RoutedEventArgs e)
        {
            var bankMoneyWindow = new BankMoneyWindow();
            bankMoneyWindow.Show();
        }

        private void ClientsButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void ATMButton_Click(object sender, RoutedEventArgs e)
        {
            var atmWindow = new ATMWindow();
            atmWindow.Show();
        }
    }
}
