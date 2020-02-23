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
    /// Логика взаимодействия для BankMoneyWindow.xaml
    /// </summary>
    public partial class BankMoneyWindow : Window
    {
        public BankMoneyWindow()
        {
            this.DataContext = GetBankAccount();
            InitializeComponent();
        }

        private BankResourse GetBankAccount()
        {
            using(var db = new ClientsEntities())
            {
                var resources = db.BankResourse.ToList();
                return resources[0];
            }
        }
    }
}
