using System.Text.RegularExpressions;
using Domain.Extensions;

namespace UI.ViewModels
{
    public class PersonViewModel
    {
        private string
            _firstName = string.Empty,
            _lastName = string.Empty,
            _email = string.Empty,
            _confirmEmail = string.Empty,
            _phone = string.Empty,
            _identifier = string.Empty,
            _address = string.Empty,
            _city = string.Empty,
            _number = string.Empty,
            _district = string.Empty,
            _state = string.Empty,
            _country = string.Empty,
            _zipCode = string.Empty,
            _complement = string.Empty,
            _motherName = string.Empty,
            _fatherName = string.Empty,
            _responsiblePhone = string.Empty,
            _responsibleEmail = string.Empty,
            _responsibleConfirmEmail = string.Empty;

        private DateTime _dateBirth;

        public string Identifier
        {
            get => _identifier;
            set => _identifier = value;
        }

        public string FirstName
        {
            get => _firstName;
            set => _firstName = value.CleanInput();
        }

        public string LastName
        {
            get => _lastName;
            set => _lastName = value.CleanInput();
        }

        public string Email
        {
            get => _email;
            set
            {
                string cleanedValue = Regex.Replace(
                    value.Trim().ToLower(), @"[^0-9-@.a-zA-Z]", string.Empty);
                _email = cleanedValue;
            }
        }

        public string ConfirmEmail
        {
            get => _confirmEmail;
            set
            {
                string cleanedValue = Regex.Replace(
                    value.Trim().ToLower(), @"[^0-9-@.a-zA-Z]", string.Empty);
                _confirmEmail = cleanedValue;
            }
        }

        public DateTime DateBirth
        {
            get => _dateBirth;
            set
            {
                DateTime dateFormated = new(value.Year, value.Month, value.Day);
                _dateBirth = dateFormated;
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                string cleanedValue = Regex.Replace(value, @"[^0-9()\s-]", string.Empty);
                _phone = cleanedValue;
            }
        }


        public string Address
        {
            get => _address;
            set => _address = value.CleanInput();
        }

        public string City
        {
            get => _city;
            set => _city = value.CleanInput();
        }

        public string Number
        {
            get => _number;
            set
            {
                string cleanedValue = Regex.Replace(value, @"[^0-9]", string.Empty);
                _number = cleanedValue;
            }
        }
        public string District
        {
            get => _district;
            set => _district = value.CleanInput();
        }

        public string State
        {
            get => _state;
            set => _state = value.CleanInput();
        }

        public string Country
        {
            get => _country;
            set => _country = value.CleanInput();
        }

        public string ZipCode
        {
            get => _zipCode;
            set => _zipCode = Regex.Replace(value, @"[^0-9-]", string.Empty);
        }

        public string Complement
        {
            get => _complement;
            set => _complement = value.CleanInput();
        }

        public string MotherName
        {
            get => _motherName;
            set => _motherName = value.CleanInput();
        }

        public string FatherName
        {
            get => _fatherName;
            set => _fatherName = value.CleanInput();
        }

        public string ResponsiblePhone
        {
            get => _responsiblePhone;
            set
            {
                string cleanedValue = Regex.Replace(value, @"[^0-9()\s-]", string.Empty);
                _responsiblePhone = cleanedValue;
            }
        }

        public string ResponsibleConfirmEmail
        {
            get => _responsibleConfirmEmail;
            set
            {
                string cleanedValue = Regex.Replace(
                    value.Trim().ToLower(), @"[^0-9-@.a-zA-Z]", string.Empty);
                _responsibleConfirmEmail = cleanedValue;
            }
        }

        public string ResponsibleEmail
        {
            get => _responsibleEmail;
            set
            {
                string cleanedValue = Regex.Replace(
                    value.Trim().ToLower(), @"[^0-9-@.a-zA-Z]", string.Empty);
                _responsibleEmail = cleanedValue;
            }
        }
    }
}
