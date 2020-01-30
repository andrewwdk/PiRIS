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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace Clients
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ClientViewModel _client;
        public MainWindow()
        {
            _client = new ClientViewModel();
            this.DataContext = _client;
            InitializeComponent();
            Load();
        }

        public ClientViewModel Client { get { return _client; } }

        private void Load()
        {
            //using(ClientsEntities db = new ClientsEntities())
            //{
            //    Client.Nationalities = db.Nationality.ToList<Nationality>();
            //    Client.Cities = db.City.ToList<City>();
            //    Client.Disabilities = db.Disability.ToList<Disability>();
            //    Client.MaritualStatuses = db.MaritualStatus.ToList<MaritualStatus>();
            //}
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            var a = Client.Surname;
            if (PhoneNumberMaskedTextbox.IsMaskCompleted)
            {
                var t = true;
            }
        }
    }
}
