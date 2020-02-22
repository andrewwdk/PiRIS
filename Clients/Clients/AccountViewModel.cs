using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Clients
{
    public class AccountViewModel : INotifyPropertyChanged
    {
        private string _surname;
        private string _name;
        private string _patronimic;
        private string _depositType;
        private string _accountNumber;
        private string _moneyAmount;
        private DateTime _startDate;
        private DateTime _endDate;
        private string _daysCount;
        private string _percents;
        private string _currency;
        private string _isPercentage;
        private string _percentMoney;
        private string _isClosed;

        public AccountViewModel(Account account)
        {
            using (var db = new ClientsEntities())
            {
                var client = db.GetClientById(account.ClientID);
                _surname = client?.Surname;
                _name = client?.Name;
                _patronimic = client?.Patronimic;
                var depositType = db.GetDepositTypeById(account.DepositTypeID);
                _depositType = depositType.Name;
                _accountNumber = account.AccountNumber;
                _moneyAmount = account.MoneyAmount.ToString();
                _startDate = account.StartDate;
                _endDate = account.EndDate;
                _daysCount = account.DaysCount.ToString();
                _percents = depositType.Percents.ToString();
                _currency = db.GetCurrencyById(account.CurrencyID);
                _isPercentage = (account.PercentAccountID != null) ? "Да" : "Нет";
                if (account.PercentAccountID != null)
                {
                    var percentAcc = db.GetAccountById(account.PercentAccountID.Value);
                    _percentMoney = percentAcc.MoneyAmount.ToString();
                }
                _isClosed = account.IsClosed ? "Да" : "Нет";
            } 
        }

        public int Id { get; set; }

        public string SNP
        {
            get
            {
                return _surname + " " + Char.ToUpper(_name[0]) + ". " + Char.ToUpper(_patronimic[0]) + ".";
            }
        }

        public string IsClosed
        {
            get { return _isClosed; }
        }

        public string IsPercentage
        {
            get { return _isPercentage; }
        }

        public string PercentMoney
        {
            get { return _percentMoney; }
        }

        public string Surname
        {
            get { return _surname; }
        }

        public string Name
        {
            get { return _name; }
        }

        public string Patronimic
        {
            get { return _patronimic; }
        }

        public string DepositType
        {
            get { return _depositType; }
            set
            {
                if (_depositType != value)
                {
                    _depositType = value;
                    OnPropertyChanged("DepositType");
                }
            }
        }

        public string AccountNumber
        {
            get { return _accountNumber; }
        }

        public string MoneyAmount
        {
            get { return _moneyAmount; }
            set
            {
                if (_moneyAmount != value)
                {
                    _moneyAmount = value;
                    OnPropertyChanged("MoneyAmount");
                }
            }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged("StartDate");
                }
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    OnPropertyChanged("EndDate");
                }
            }
        }

        public string DaysCount
        {
            get { return _daysCount; }
        }

        public string Percents
        {
            get { return _percents; }
        }

        public string Currency
        {
            get { return _currency; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
