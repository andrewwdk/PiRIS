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
            DepositTypeComboBox.ItemsSource = GetDepositTypes();
            CurrencyComboBox.ItemsSource = GetCurrencies();
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

        private List<DepositType> GetDepositTypes()
        {
            List<DepositType> list = new List<DepositType>();

            using (var db = new ClientsEntities())
            {
                foreach (var type in db.DepositType)
                {
                    list.Add(type);
                }
            }

            return list;
        }

        private List<Currency> GetCurrencies()
        {
            List<Currency> list = new List<Currency>();

            using (var db = new ClientsEntities())
            {
                foreach (var cur in db.Currency)
                {
                    list.Add(cur);
                }
            }

            return list;
        }

        private void DepositTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var db = new ClientsEntities())
            {
                _accountModel.Percents = db.GetDepositTypeByName(_accountModel.DepositType).Percents.ToString();
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsDataCorrect())
            {
                using (var db = new ClientsEntities())
                {
                    var mainAccount = new Account();
                    var percentAccount = new Account();
                    mainAccount.ClientID = _accountModel.ClientId;
                    percentAccount.ClientID = _accountModel.ClientId;
                    mainAccount.DepositTypeID = db.GetDepositTypeByName(_accountModel.DepositType).DepositTypeID;
                    percentAccount.DepositTypeID = mainAccount.DepositTypeID;
                    mainAccount.AccountNumber = _accountModel.AccountNumber;
                    var part1 = _accountModel.AccountNumber.Substring(0, 9);
                    var part2 = GetCorrectAccountCountPart(Convert.ToInt32(_accountModel.AccountNumber.Substring(9, 3)) + 1);
                    var part3 = new Random().Next(0, 9);
                    percentAccount.AccountNumber = part1 + part2 + part3;
                    mainAccount.MoneyAmount = Convert.ToDouble(_accountModel.MoneyAmount);
                    if (mainAccount.DepositTypeID == 3 || mainAccount.DepositTypeID == 4)
                    {
                        percentAccount.MoneyAmount = Math.Round(mainAccount.MoneyAmount * Convert.ToDouble(_accountModel.Percents) / 100, 2);
                    }
                    else
                    {
                        percentAccount.MoneyAmount = 0;
                    }
                    mainAccount.StartDate = _accountModel.StartDate;
                    percentAccount.StartDate = _accountModel.StartDate;
                    mainAccount.EndDate = _accountModel.EndDate;
                    percentAccount.EndDate = _accountModel.EndDate;
                    mainAccount.DaysCount = 0;
                    percentAccount.DaysCount = 0;
                    mainAccount.CurrencyID = db.GetCurrencyByName(_accountModel.Currency).CurrencyID;
                    percentAccount.CurrencyID = mainAccount.CurrencyID;
                    mainAccount.IsClosed = false;
                    percentAccount.IsClosed = false;
                    db.Account.Add(percentAccount);
                    db.SaveChanges();
                    mainAccount.PercentAccountID = db.GetAccountByAccountNumber(percentAccount.AccountNumber).AccountID;
                    db.Account.Add(mainAccount);
                    var bankResources = db.BankResourse.ToList();
                    if (mainAccount.DepositTypeID == 3 || mainAccount.DepositTypeID == 4)
                    {
                        bankResources[0].PhysicalMoney -= ConvertCurrencyToByn(mainAccount.CurrencyID) * mainAccount.MoneyAmount;
                    }
                    else
                    {
                        bankResources[0].PhysicalMoney += ConvertCurrencyToByn(mainAccount.CurrencyID) * mainAccount.MoneyAmount;
                    }
                    db.SaveChanges();
                    if (mainAccount.DepositTypeID == 3 || mainAccount.DepositTypeID == 4)
                    {
                        var additionalAcc = new Account();
                        part1 = _accountModel.AccountNumber.Substring(0, 9);
                        part2 = GetCorrectAccountCountPart(Convert.ToInt32(_accountModel.AccountNumber.Substring(9, 3)) + 1);
                        part3 = new Random().Next(0, 9);
                        additionalAcc.AccountNumber = part1 + part2 + part3;
                        additionalAcc.ClientID = mainAccount.ClientID;
                        additionalAcc.CurrencyID = mainAccount.CurrencyID;
                        additionalAcc.DaysCount = 0;
                        additionalAcc.DepositTypeID = 1;
                        additionalAcc.IsClosed = true;
                        additionalAcc.StartDate = mainAccount.StartDate;
                        additionalAcc.EndDate = mainAccount.EndDate;
                        additionalAcc.MoneyAmount = mainAccount.MoneyAmount;
                        db.Account.Add(additionalAcc);
                        db.SaveChanges();
                        var card = new Card();
                        card.AccountID = db.GetAccountByAccountNumber(additionalAcc.AccountNumber).AccountID;
                        card.Number = mainAccount.AccountNumber + new Random().Next(100, 999);
                        card.PIN = new Random().Next(1000, 9999);
                        db.Card.Add(card);
                    }
                    db.SaveChanges();
                    this.Close();
                }
            }
        }

        private double ConvertCurrencyToByn(int id)
        {
            if (id == 2) return 2.2;
            if (id == 3) return 2.4;

            return 1;
        }

        private bool IsDataCorrect()
        {
            if(_accountModel.Name == null || _accountModel.Name == string.Empty)
            {
                MessageBox.Show("Выберите клиента!");
                return false;
            }

            if(_accountModel.DepositType == null)
            {
                MessageBox.Show("Выберите вид депозита!");
                return false;
            }

            if (_accountModel.Currency == null)
            {
                MessageBox.Show("Выберите валюту!");
                return false;
            }

            if(_accountModel.StartDate.Year < 2020)
            {
                MessageBox.Show("Выберите корректную дату открытия!");
                return false;
            }

            if (_accountModel.EndDate.Year < 2020 || _accountModel.EndDate <= _accountModel.StartDate)
            {
                MessageBox.Show("Выберите корректную дату закрытия!");
                return false;
            }

            float temp;
            if (_accountModel.MoneyAmount == null || _accountModel.MoneyAmount == string.Empty || !float.TryParse(_accountModel.MoneyAmount, out temp))
            {
                MessageBox.Show("Введите корректную сумму вклада!");
                return false;
            }

            return true;
        }
    }
}
