using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Clients
{
    class AccountViewModel : INotifyPropertyChanged
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

        public int Id { get; set; }
        public string Surname
        {
            get { return _surname; }
            set
            {
                if (_surname != value)
                {
                    _surname = value;
                    OnPropertyChanged("Surname");
                }
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string Patronimic
        {
            get { return _patronimic; }
            set
            {
                if (_patronimic != value)
                {
                    _patronimic = value;
                    OnPropertyChanged("Patronimic");
                }
            }
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
            set
            {
                if (_accountNumber != value)
                {
                    _accountNumber = value;
                    OnPropertyChanged("AccountNumber");
                }
            }
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
            set
            {
                if (_daysCount != value)
                {
                    _daysCount = value;
                    OnPropertyChanged("DaysCount");
                }
            }
        }

        public string Percents
        {
            get { return _percents; }
            set
            {
                if (_percents != value)
                {
                    _percents = value;
                    OnPropertyChanged("Percents");
                }
            }
        }

        public string Currency
        {
            get { return _currency; }
            set
            {
                if (_currency != value)
                {
                    _currency = value;
                    OnPropertyChanged("Currency");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
