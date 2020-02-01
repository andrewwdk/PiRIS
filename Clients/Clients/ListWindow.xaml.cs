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
    /// Логика взаимодействия для ListWindow.xaml
    /// </summary>
    public partial class ListWindow : Window
    {
        private ClientViewModel _client;

        public ListWindow(ClientViewModel client)
        {
            _client = client;
            InitializeComponent();
            Load();
        }

        private void Load()
        {
            using(ClientsEntities db = new ClientsEntities())
            {
                ClientListDataGrid.ItemsSource = db.Client
                    .ToList<Client>()
                    .Where(c => c.Surname != null)
                    .OrderBy(s => s.Surname)
                    .ThenBy(s => s.Name)
                    .ThenBy(s => s.Patronimic);
            }
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedClientID = (ClientListDataGrid.SelectedItem as Client).ClientID;
            Client selectedClient;

            using(ClientsEntities db = new ClientsEntities())
            {
                selectedClient = db.GetClientById(selectedClientID);

                if (selectedClient != null)
                {
                    InitializeClient(selectedClient, db);
                }
            }

            this.Close();
        }

        private void InitializeClient(Client selectedClient, ClientsEntities db)
        {
            _client.Id = selectedClient.ClientID;
            _client.Surname = selectedClient.Surname;
            _client.Name = selectedClient.Name;
            _client.Patronimic = selectedClient.Patronimic;
            _client.BirthDate = selectedClient.BirthDate;
            _client.PassportSeries = selectedClient.PassportSeries;
            _client.PassportNumber = selectedClient.PassportNumber;
            _client.Authority = selectedClient.Authority;
            _client.IssueDate = selectedClient.DateOfIssue;
            _client.PlaceOfBirth = selectedClient.PlaceOfBirth;
            _client.IdentificationNumber = selectedClient.IdentificationNumber;
            _client.Location = db.GetCityById(selectedClient.Location);
            _client.Address = selectedClient.Address;
            _client.MobileNumber = selectedClient.MobileNumber;
            _client.PhoneNumber = selectedClient.PhoneNumber == null ? null : FormatePhoneNumber(selectedClient.PhoneNumber);
            _client.Email = selectedClient.Email;
            _client.MaritualStatus = db.GetMaritualStatusById(selectedClient.MaritalStatus);
            _client.Disability = db.GetDisabilityById(selectedClient.Disability);
            _client.Nationality = db.GetNationalityById(selectedClient.Nationality);
            _client.Pensioner = selectedClient.Pensioner;
            _client.RegistrationCity = db.GetCityById(selectedClient.RegistrationCity);
            _client.MaleGender = (selectedClient.Gender) ? true : false;
            _client.FemaleGender = (selectedClient.Gender) ? false : true;
            _client.MonthlyIncome = selectedClient.MonthlyIncome == null ? null : FormateMonthlyIncome(selectedClient.MonthlyIncome.ToString());
        }

        private string FormatePhoneNumber(string initialValue)
        {
            var newValue = initialValue;
            var startPrefixIndex = newValue.IndexOf("(");
            var endPrefixIndex = newValue.IndexOf(")");
            var difference = endPrefixIndex - startPrefixIndex;
            var underscoreCountNeeded = 5 - difference;

            for (int i = 0; i < underscoreCountNeeded; i++)
            {
                newValue = newValue.Insert(endPrefixIndex, "_");
            }

            underscoreCountNeeded = 17 - newValue.Length;

            for (int i = 0; i < underscoreCountNeeded; i++)
            {
                newValue = newValue + "_";
            }

            return newValue;
        }

        private string FormateMonthlyIncome(string initialValue)
        {
            var newValue = "BYN   " + initialValue.Substring(0, initialValue.Length - 2).Replace(",", ".");

            var pointIndex = newValue.IndexOf(".");
            var underscoreCountNeeded = 12 - pointIndex;

            for (int i = 0; i < underscoreCountNeeded; i++)
            {
                newValue = newValue.Insert(pointIndex, "_");
            }

            return newValue;
        }
    }
}
