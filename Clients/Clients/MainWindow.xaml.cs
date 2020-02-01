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
using System.Globalization;

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

        public ClientViewModel Client { get { return _client; } private set { _client = value; } }

        private void Load()
        {
            using (ClientsEntities db = new ClientsEntities())
            {
                Client.Nationalities = db.Nationality.ToList<Nationality>();
                Client.Cities = db.City.ToList<City>();
                Client.Disabilities = db.Disability.ToList<Disability>();
                Client.MaritualStatuses = db.MaritualStatus.ToList<MaritualStatus>();
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DataPreprocess();
            if (!IsDataCorrect())
            {
                return;
            }

            if (IdentificationNumberExists())
            {
                MessageBox.Show("Клиент с таким идентификационным номером уже существует!");
            }
            else
            {
                AddClient();
                MessageBox.Show("Клиент успешно добавлен!");
                ClearFields();
            }
        }

        private void AddClient()
        {
            using (ClientsEntities db = new ClientsEntities())
            {
                var client = new Client();
                InitializeClientForDb(client, db);
                db.Client.Add(client);
                db.SaveChanges();
            }
        }

        private void InitializeClientForDb(Client client, ClientsEntities db)
        {
            client.Surname = Client.Surname;
            client.Name = Client.Name;
            client.Patronimic = Client.Patronimic;
            client.BirthDate = Client.BirthDate;
            client.PassportSeries = Client.PassportSeries;
            client.PassportNumber = Client.PassportNumber;
            client.Authority = Client.Authority;
            client.DateOfIssue = Client.IssueDate;
            client.PlaceOfBirth = Client.PlaceOfBirth;
            client.IdentificationNumber = Client.IdentificationNumber;
            client.Location = db.GetCityIDByValue(Client.Location);
            client.Address = Client.Address;
            client.MobileNumber = Client.MobileNumber;
            client.PhoneNumber = Client.PhoneNumber.Replace("_", "");
            client.Email = Client.Email;
            client.MaritalStatus = db.GetMaritualStatusIDByValue(Client.MaritualStatus);
            client.Disability = db.GetDisabilityIDByValue(Client.Disability);
            client.Nationality = db.GetNationalityIDByValue(Client.Nationality);
            client.Pensioner = Client.Pensioner;
            client.RegistrationCity = db.GetCityIDByValue(Client.RegistrationCity);
            client.Gender = (Client.MaleGender) ? true : false;
            client.MonthlyIncome = ConvertStringToMoneyValue(Client.MonthlyIncome);
        }

        private decimal? ConvertStringToMoneyValue(string value)
        {
            if(value == null)
            {
                return null;
            }

            var monthlyIncome = Client.MonthlyIncome.Substring(6).Replace("_", "");
            if (monthlyIncome.EndsWith("."))
            {
                monthlyIncome = monthlyIncome.Replace(".", "");
            }

            return Convert.ToDecimal(monthlyIncome, new CultureInfo("en-US"));
        }

        private void ClearFields()
        {
            Client.Surname = null;
            Client.Name = null;
            Client.Patronimic = null;
            Client.BirthDate = DateTime.MinValue;
            Client.PassportSeries = null;
            Client.PassportNumber = null;
            Client.Authority = null;
            Client.IssueDate = DateTime.MinValue;
            Client.PlaceOfBirth = null;
            Client.IdentificationNumber = null;
            Client.Location = null;
            Client.Address = null;
            Client.MobileNumber = null;
            Client.PhoneNumber = null;
            Client.Email = null;
            Client.MaritualStatus = null;
            Client.Disability = null;
            Client.Nationality = null;
            Client.Pensioner = false;
            Client.RegistrationCity = null;
            Client.MaleGender = true;
            Client.FemaleGender = false;
            Client.MonthlyIncome = null;
        }

        private bool IdentificationNumberExists()
        {
            using (ClientsEntities db = new ClientsEntities())
            {
                var clients = db.Client.ToList<Client>();
                foreach(var client in clients)
                {
                    if(client.IdentificationNumber == Client.IdentificationNumber)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsDataCorrect()
        {
            if (Client.Surname == null || Client.Surname == string.Empty || !Client.Surname.All(c => Char.IsLetter(c) || c == '-'))
            {
                MessageBox.Show("Введите корректную фамилию клиента!");
                return false;
            }

            if (Client.Name == null || Client.Name == string.Empty || !Client.Name.All(c => Char.IsLetter(c) || c == '-' || c == ' '))
            {
                MessageBox.Show("Введите корректное имя клиента!");
                return false;
            }

            if (Client.Patronimic == null || Client.Patronimic == string.Empty || !Client.Patronimic.All(c => Char.IsLetter(c)))
            {
                MessageBox.Show("Введите корректное отчество клиента!");
                return false;
            }

            if (Client.BirthDate.Year < 1900)
            {
                MessageBox.Show("Введите корректную дату рождения клиента!");
                return false;
            }

            if (Client.PassportSeries == null || Client.PassportSeries == string.Empty || !Client.PassportSeries.All(c => Char.IsLetter(c)))
            {
                MessageBox.Show("Введите корректную серию паспорта клиента!");
                return false;
            }

            if (!Client.MaleGender && !Client.FemaleGender)
            {
                MessageBox.Show("Укажите пол клиента!");
                return false;
            }

            if (!PassportNumberMaskedTextbox.IsMaskCompleted)
            {
                MessageBox.Show("Введите корректный номер паспорта клиента!");
                return false;
            }

            if (Client.Authority == null || Client.Authority == string.Empty || !Client.Authority.All(c => Char.IsLetter(c) || c == ' '))
            {
                MessageBox.Show("Укажите корректно, кем выдан паспорт клиента!");
                return false;
            }

            if (Client.IssueDate.Year < 1900)
            {
                MessageBox.Show("Введите корректную дату выдачи паспорта клиента!");
                return false;
            }

            if (!IdentificationNumberMaskedTextbox.IsMaskCompleted)
            {
                MessageBox.Show("Введите корректный идентификационный номер паспорта клиента!");
                return false;
            }

            if (Client.PlaceOfBirth == null || Client.PlaceOfBirth == string.Empty)
            {
                MessageBox.Show("Введите корректное место рождения клиента!");
                return false;
            }

            if (Client.Location == null)
            {
                MessageBox.Show("Введите корректное место проживания клиента!");
                return false;
            }

            if (Client.Address == null || Client.Address == string.Empty)
            {
                MessageBox.Show("Введите корректный адрес фактического проживания клиента!");
                return false;
            }

            if (!PhoneNumberMaskedTextbox.IsMaskCompleted && Client.PhoneNumber != null)
            {
                MessageBox.Show("Введите корректный домашний телефон клиента!");
                return false;
            }

            if (!MobileNumberMaskedTextbox.IsMaskCompleted && Client.MobileNumber != null)
            {
                MessageBox.Show("Введите корректный мобильный телефон клиента!");
                return false;
            }

            if (Client.RegistrationCity == null)
            {
                MessageBox.Show("Введите корректный город прописки клиента!");
                return false;
            }

            if (Client.Nationality == null)
            {
                MessageBox.Show("Укажите гражданство клиента!");
                return false;
            }

            if (Client.MaritualStatus == null)
            {
                MessageBox.Show("Укажите семейное положение клиента!");
                return false;
            }

            if (Client.Disability == null)
            {
                MessageBox.Show("Укажите инвалидность клиента!");
                return false;
            }

            if (!MonthlyIncomeMaskedTextbox.IsMaskCompleted && Client.MonthlyIncome != null)
            {
                MessageBox.Show("Введите корректный месячный доход клиента!");
                return false;
            }

            return true;
        }

        private void DataPreprocess()
        {
            Client.Surname = Client.Surname?.Trim();
            Client.Name = Client.Name?.Trim();
            Client.Patronimic = Client.Patronimic?.Trim();
            Client.PassportSeries= Client.PassportSeries?.Trim();
            Client.Authority = Client.Authority?.Trim();
            Client.PlaceOfBirth = Client.PlaceOfBirth?.Trim();
            Client.Address = Client.Address?.Trim();
            Client.Email = Client.Email?.Trim();
            Client.IdentificationNumber = Client.IdentificationNumber?.ToUpper();

            if(Client.MonthlyIncome == "BYN   ______.__")
            {
                Client.MonthlyIncome = null;
            }

            if(Client.MobileNumber == "+375 (__) ___-__-__")
            {
                Client.MobileNumber = null;
            }

            if(Client.PhoneNumber == "80 (____) _______")
            {
                Client.PhoneNumber = null;
            }

            if(Client.Email == string.Empty)
            {
                Client.Email = null;
            }
        }

        private void ListButton_Click(object sender, RoutedEventArgs e)
        {
            ListWindow listWindow = new ListWindow(Client);
            listWindow.Show();
        }
    }
}
