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
    /// Логика взаимодействия для ATMWindow.xaml
    /// </summary>
    public partial class ATMWindow : Window
    {
        private Card _card;
        private Account _account;
        Action savedOperation = null;
        double cash;
        string phoneNumber;

        public ATMWindow()
        {
            InitializeComponent();
            InitialState();
        }

        private void InitialState()
        {
            InputTextBox.Text = string.Empty;
            InfoTextBox.Text = "Введите номер карты.";
            Button1.Content = "Ввод";
            Button2.Content = string.Empty;
            Button3.Content = string.Empty;
            Button1.Click -= CheckCardPIN;
            Button1.Click -= RepeateCheckCardPin;
            Button1.Click += CheckCardNumber;
            Button2.Click -= RepeateCheckCardPin;
            Button3.Click -= RepeateCheckCardPin;
            _account = null;
            _card = null;
        }

        private void WaitingForPINState()
        {
            InputTextBox.Text = string.Empty;
            InfoTextBox.Text = "Введите пароль.";
            Button1.Content = "Ввод";
            Button2.Content = string.Empty;
            Button3.Content = string.Empty;
            Button1.Click += CheckCardPIN;
        }

        private void MenuState()
        {
            InputTextBox.Text = string.Empty;
            InfoTextBox.Text = "Выберите действие.";
            Button1.Content = "Остаток";
            Button1.Click += RepeateCheckCardPin;
            Button2.Content = "Снять";
            Button2.Click += RepeateCheckCardPin;
            Button3.Content = "Моб. связь";
            Button3.Click += RepeateCheckCardPin;
            cash = 0;
            phoneNumber = null;
        }

        private void ShowMoneyAmountState()
        {
            InputTextBox.Text = string.Empty;
            using (var db = new ClientsEntities())
            {
                var acc = db.GetAccountById(_account.AccountID);
                InfoTextBox.Text = "Сумма средств: " + acc.MoneyAmount + ". Печатать чек?";
            }
            Button1.Content = "Да";
            Button1.Click += PrintCheque;
            Button2.Content = "Нет";
            Button2.Click += DontPrintCheque;
            Button3.Content = string.Empty;
        }

        private void GetCashState()
        {
            InputTextBox.Text = string.Empty;
            InfoTextBox.Text = "Введите сумму(в белорусских рублях).";
            Button1.Content = "Ввод";
            Button1.Click += CashAmountEntered;
            Button2.Content = string.Empty;
            Button3.Content = string.Empty;
        }

        private void EnteringPhoneNumberState()
        {
            InputTextBox.Text = "+375";
            InfoTextBox.Text = "Введите номер.";
            Button1.Content = "Ввод";
            Button1.Click += CheckPhoneNumber;
            Button2.Content = string.Empty;
            Button3.Content = string.Empty;
        }

        private void EnteringMoneyForMobileState()
        {
            InputTextBox.Text = string.Empty;
            InfoTextBox.Text = "Введите сумму(в белорусских рублях).";
            Button1.Content = "Ввод";
            Button1.Click += MoneyForMobileEntered;
            Button2.Content = string.Empty;
            Button3.Content = string.Empty;
        }

        private void CheckCardNumber(object sender, RoutedEventArgs e)
        {
            string text = InputTextBox.Text;
            if(IsDigitsOnly(text) && text.Length == 16)
            {
                using(var db = new ClientsEntities())
                {
                    _card = db.GetCardByNumber(text);
                    if(_card != null)
                    {
                        _account = db.GetAccountById(_card.AccountID);
                        WaitingForPINState();
                        Button1.Click -= CheckCardNumber;
                    }
                    else
                    {
                        InfoTextBox.Text = "Карта с таким номером не найдена.";
                    }
                }
            }
            else
            {
                InfoTextBox.Text = "Введите корректный номер карты.";
            }
        }

        private void CheckCardPIN(object sender, RoutedEventArgs e)
        {
            string text = InputTextBox.Text;
            if (text == _card.PIN.ToString())
            {
                if (savedOperation != null)
                {
                    savedOperation();
                    savedOperation = null;
                }
                else
                {
                    MenuState();
                }
                Button1.Click -= CheckCardPIN;
            }
            else
            {
                InfoTextBox.Text = "Пароль неверный. Повторите попытку.";
            }
        }

        private void RepeateCheckCardPin(object sender, RoutedEventArgs e)
        {
            WaitingForPINState();
            if ((Button)sender == Button1)
            {
                savedOperation = ShowMoneyAmount;
            }
            if ((Button)sender == Button2)
            {
                savedOperation = GetCash;
            }
            if ((Button)sender == Button3)
            {
                savedOperation = Mobile;
            }
            Button1.Click -= RepeateCheckCardPin;
            Button2.Click -= RepeateCheckCardPin;
            Button3.Click -= RepeateCheckCardPin;
        }

        private void ShowMoneyAmount()
        {
            ShowMoneyAmountState();
        }

        private void GetCash()
        {
            GetCashState();
        }

        private void Mobile()
        {
            EnteringPhoneNumberState();
        }

        private void PrintCheque(object sender, RoutedEventArgs e)
        {
            PrintCheque();
            MenuState();
            Button1.Click -= PrintCheque;
            Button2.Click -= DontPrintCheque;
        }

        private void DontPrintCheque(object sender, RoutedEventArgs e)
        {
            MenuState();
            Button1.Click -= PrintCheque;
            Button2.Click -= DontPrintCheque;
        }

        private void CashAmountEntered(object sender, RoutedEventArgs e)
        {
            Button1.Click -= CashAmountEntered;
            double sum;
            if(double.TryParse(InputTextBox.Text, out sum))
            {
                if (_account.MoneyAmount * ConvertCurrencyToByn(_account.CurrencyID) > sum)
                {
                    using (var db = new ClientsEntities())
                    {
                        var acc = db.GetAccountById(_account.AccountID);
                        acc.MoneyAmount -= sum / ConvertCurrencyToByn(_account.CurrencyID);
                        db.SaveChanges();
                        cash = sum;
                        ShowMoneyAmountState();
                    }
                }
                else
                {
                    MessageBox.Show("На вашем счёте недостаточно средств.");
                    MenuState();
                }
            }
            else
            {
                MessageBox.Show("Введите корректную сумму.");
                GetCashState();
            }
        }

        private void CheckPhoneNumber(object sender, RoutedEventArgs e)
        {
            Button1.Click -= CheckPhoneNumber;
            string text = InputTextBox.Text;
            if (IsDigitsOnly(text.Substring(1)) && text.Substring(0, 4) == "+375" && text.Length == 13)
            {
                phoneNumber = text;
                EnteringMoneyForMobileState();
            }
            else
            {
                MessageBox.Show("Введите корректный номер телефона.");
                EnteringPhoneNumberState();
            }
        }

        private void MoneyForMobileEntered(object sender, RoutedEventArgs e)
        {
            Button1.Click -= MoneyForMobileEntered;
            double sum;
            if (double.TryParse(InputTextBox.Text, out sum))
            {
                if (_account.MoneyAmount * ConvertCurrencyToByn(_account.CurrencyID) > sum)
                {
                    using (var db = new ClientsEntities())
                    {
                        var acc = db.GetAccountById(_account.AccountID);
                        acc.MoneyAmount -= sum / ConvertCurrencyToByn(_account.CurrencyID);
                        db.SaveChanges();
                        cash = sum;
                        MessageBox.Show("Оплата мобильной связи. Номер телефона: " + phoneNumber + "; Дата: " + DateTime.Now + "; Номер карты: " + _card.Number 
                            + "; Сумма на счёте: " + acc.MoneyAmount + "; Денег снято: " + cash + "BYN.");
                        MenuState();
                    }
                }
                else
                {
                    MessageBox.Show("На вашем счёте недостаточно средств.");
                    MenuState();
                }
            }
            else
            {
                MessageBox.Show("Введите корректную сумму.");
                EnteringMoneyForMobileState();
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            InitialState();
        }

        private void PrintCheque()
        {
            using (var db = new ClientsEntities())
            {
                var currency = db.GetCurrencyById(_account.CurrencyID);
                var acc = db.GetAccountById(_account.AccountID);
                MessageBox.Show("Дата: " + DateTime.Now + "; Номер карты: " + _card.Number + "; Сумма на счёте: " + acc.MoneyAmount + "; Денег снято: " + cash + currency + ".");
            }
        }

        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        private double ConvertCurrencyToByn(int id)
        {
            if (id == 2) return 2.2;
            if (id == 3) return 2.4;

            return 1;
        }
    }
}
