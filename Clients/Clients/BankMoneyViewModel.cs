using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Clients
{
    public class BankMoneyViewModel : INotifyPropertyChanged
    {
        public BankMoneyViewModel(BankResourse resource)
        {
            RealMoney = resource.RealMoney;
            PhysicalMoney = resource.PhysicalMoney;
        }

        public double RealMoney { get; set; }
        public double PhysicalMoney { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
