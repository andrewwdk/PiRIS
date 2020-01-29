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
            //PassportNumberMaskedTextbox.Mask = "0000000";
            //PassportNumberMaskedTextbox.Value = "1111111";//"{Binding Path=MyClass.Value1, Mode=TwoWay}";
            //SqlConnection connect = new SqlConnection();
            //connect.ConnectionString = @"data source = DESKTOP-76KUOA5\SQLEXPRESS;database = Clients;integrated security = SSPI";
            //SqlCommand cmd = new SqlCommand("select * from City", connect);
            //connect.Open();
            //SqlDataReader sqr = cmd.ExecuteReader();
            //connect.Close();
        }

        public ClientViewModel Client { get { return _client; } }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
