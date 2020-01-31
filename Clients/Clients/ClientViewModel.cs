using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Clients
{
    public class ClientViewModel : INotifyPropertyChanged
    {
        private string _surname;
        private string _name;
        private string _patronimic;
        private DateTime _birthDate;
        private bool _maleGender;
        private bool _femaleGender;
        private string _passportSeries;
        private string _passportNumber;
        private string _authority;
        private DateTime _issueDate;
        private string _identificationNumber;
        private string _placeOfBirth;
        private string _location;
        private string _address;
        private string _phoneNumber;
        private string _mobileNumber;
        private string _email;
        private string _registrationCity;
        private string _maritualStatus;
        private string _nationality;
        private string _disability;
        private bool _pensioner;
        private string _monthlyIncome;
        private List<City> _cities;
        private List<Disability> _disabilities;
        private List<MaritualStatus> _maritualStatuses;
        private List<Nationality> _nationalities;

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

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                if (_birthDate != value)
                {
                    _birthDate = value;
                    OnPropertyChanged("BirthDate");
                }
            }
        }

        public bool MaleGender
        {
            get { return _maleGender; }
            set
            {
                if (_maleGender != value)
                {
                    _maleGender = value;
                    OnPropertyChanged("MaleGender");
                }
            }
        }

        public bool FemaleGender
        {
            get { return _femaleGender; }
            set
            {
                if (_femaleGender != value)
                {
                    _femaleGender = value;
                    OnPropertyChanged("FemaleGender");
                }
            }
        }

        public string PassportSeries
        {
            get { return _passportSeries; }
            set
            {
                if (_passportSeries != value)
                {
                    _passportSeries = value;
                    OnPropertyChanged("PassportSeries");
                }
            }
        }

        public string PassportNumber
        {
            get { return _passportNumber; }
            set
            {
                if (_passportNumber != value)
                {
                    _passportNumber = value;
                    OnPropertyChanged("PassportNumber");
                }
            }
        }

        public string Authority
        {
            get { return _authority; }
            set
            {
                if (_authority != value)
                {
                    _authority = value;
                    OnPropertyChanged("Authority");
                }
            }
        }

        public DateTime IssueDate
        {
            get { return _issueDate; }
            set
            {
                if (_issueDate != value)
                {
                    _issueDate = value;
                    OnPropertyChanged("IssueDate");
                }
            }
        }

        public string IdentificationNumber
        {
            get { return _identificationNumber; }
            set
            {
                if (_identificationNumber != value)
                {
                    _identificationNumber = value;
                    OnPropertyChanged("IdentificationNumber");
                }
            }
        }

        public string PlaceOfBirth
        {
            get { return _placeOfBirth; }
            set
            {
                if (_placeOfBirth != value)
                {
                    _placeOfBirth = value;
                    OnPropertyChanged("PlaceOfBirth");
                }
            }
        }

        public string Location
        {
            get { return _location; }
            set
            {
                if (_location != value)
                {
                    _location = value;
                    OnPropertyChanged("Location");
                }
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                if (_address != value)
                {
                    _address = value;
                    OnPropertyChanged("Address");
                }
            }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged("PhoneNumber");
                }
            }
        }

        public string MobileNumber
        {
            get { return _mobileNumber; }
            set
            {
                if (_mobileNumber != value)
                {
                    _mobileNumber = value;
                    OnPropertyChanged("MobileNumber");
                }
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged("Email");
                }
            }
        }

        public string RegistrationCity
        {
            get { return _registrationCity; }
            set
            {
                if (_registrationCity != value)
                {
                    _registrationCity = value;
                    OnPropertyChanged("RegistrationCity");
                }
            }
        }

        public string MaritualStatus
        {
            get { return _maritualStatus; }
            set
            {
                if (_maritualStatus != value)
                {
                    _maritualStatus = value;
                    OnPropertyChanged("MaritualStatus");
                }
            }
        }

        public string Nationality
        {
            get { return _nationality; }
            set
            {
                if (_nationality != value)
                {
                    _nationality = value;
                    OnPropertyChanged("Nationality");
                }
            }
        }

        public string Disability
        {
            get { return _disability; }
            set
            {
                if (_disability != value)
                {
                    _disability = value;
                    OnPropertyChanged("Disability");
                }
            }
        }

        public bool Pensioner
        {
            get { return _pensioner; }
            set
            {
                if (_pensioner != value)
                {
                    _pensioner = value;
                    OnPropertyChanged("Pensioner");
                }
            }
        }

        public string MonthlyIncome
        {
            get { return _monthlyIncome; }
            set
            {

                if (_monthlyIncome != value)
                {

                    _monthlyIncome = value;
                    OnPropertyChanged("MonthlyIncome");
                }
            }
        }

        public List<City> Cities
        {
            get { return _cities; }
            set
            {
                if (_cities != value)
                {
                    _cities = value;
                    OnPropertyChanged("Cities");
                }
            }
        }

        public List<Disability> Disabilities
        {
            get { return _disabilities; }
            set
            {
                if (_disabilities != value)
                {
                    _disabilities = value;
                    OnPropertyChanged("Disabilities");
                }
            }
        }

        public List<MaritualStatus> MaritualStatuses
        {
            get { return _maritualStatuses; }
            set
            {
                if (_maritualStatuses != value)
                {
                    _maritualStatuses = value;
                    OnPropertyChanged("MaritualStatuses");
                }
            }
        }

        public List<Nationality> Nationalities
        {
            get { return _nationalities; }
            set
            {
                if (_nationalities != value)
                {
                    _nationalities = value;
                    OnPropertyChanged("Nationalities");
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
