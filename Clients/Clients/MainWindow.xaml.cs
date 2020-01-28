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
        public MainWindow()
        {
            InitializeComponent();
            //SqlConnection connect = new SqlConnection();
            //connect.ConnectionString = @"data source = DESKTOP-76KUOA5\SQLEXPRESS;database = Clients;integrated security = SSPI";
            //SqlCommand cmd = new SqlCommand("select * from City", connect);
            //connect.Open();
            //SqlDataReader sqr = cmd.ExecuteReader();
            //connect.Close();
        }
    }
}
