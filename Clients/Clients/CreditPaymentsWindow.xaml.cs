using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public class AmountByDay : INotifyPropertyChanged
    {
        private DateTime _date;
        private string _amount;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged("Date");
                }
            }
        }
        public string Amount
        {
            get { return _amount; }
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    OnPropertyChanged("Amount");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    /// <summary>
    /// Логика взаимодействия для CreditPaymentsWindow.xaml
    /// </summary>
    public partial class CreditPaymentsWindow : Window
    {
        public CreditPaymentsWindow(AccountViewModel account)
        {
            InitializeComponent();
            Load(account);
        }

        public void Load(AccountViewModel account)
        {
            var list = new List<AmountByDay>();
            var daysCount = (account.EndDate - account.StartDate).Days + 1;
            var mainAmount = Convert.ToDouble(account.MoneyAmount);
            var percents = mainAmount * Convert.ToDouble(account.Percents) / 100;
            var percentsByDay = Math.Round(percents / daysCount, 2);
            var mainAmountByDay = Math.Round(mainAmount / daysCount, 2);
            double sum = 0;
            using (var db = new ClientsEntities())
            {
                if(db.GetDepositTypeByName(account.DepositType).DepositTypeID == 3)
                {
                    foreach(DateTime day in EachThirtyDays(account.StartDate, account.EndDate))
                    {
                        sum += (percentsByDay + mainAmountByDay) * 30;
                        list.Add(new AmountByDay() { Amount = ((percentsByDay + mainAmountByDay) * 30).ToString(), Date = day });
                    }
                    if (sum < mainAmount + percents)
                    {
                        list.Add(new AmountByDay() { Amount = Math.Round(mainAmount + percents - sum, 2).ToString(), Date = account.EndDate });
                    }
                }
                else
                {
                    foreach (DateTime day in EachThirtyDays(account.StartDate, account.EndDate.AddDays(-1)))
                    {
                        sum += percentsByDay * 30;
                        list.Add(new AmountByDay() { Amount = (percentsByDay * 30).ToString(), Date = day });
                    }
                    list.Add(new AmountByDay() { Date = account.EndDate, Amount = Math.Round(mainAmount + percents - sum, 2).ToString() });
                }
            }
            CreditPaymentsDataGrid.ItemsSource = list;
        }

        public IEnumerable<DateTime> EachThirtyDays(DateTime from, DateTime thru)
        {
            for (var day = from.Date.AddDays(29); day.Date <= thru.Date; day = day.AddDays(30))
                yield return day;
        }
    }
}
