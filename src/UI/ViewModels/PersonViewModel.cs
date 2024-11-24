using System.Text.RegularExpressions;

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
            _dateBirth = string.Empty,
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

        public string Identifier
        {
            get => _identifier;
            set => _identifier = value;
        }

        public string FirstName
        {
            get => _firstName;
            set => _firstName = value;
        }

        public string LastName
        {
            get => _lastName;
            set => _lastName = value;
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

        public string DateBirth
        {
            get => _dateBirth;
            set
            {
                _dateBirth = value;
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
            set => _address = value;
        }

        public string City
        {
            get => _city;
            set => _city = value;
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
            set => _district = value;
        }

        public string State
        {
            get => _state;
            set => _state = value;
        }

        public string Country
        {
            get => _country;
            set
            {
                _country = value;
                _country = "Brasil";
            }
        }

        public string ZipCode
        {
            get => _zipCode;
            set => _zipCode = Regex.Replace(value, @"[^0-9-]", string.Empty);
        }

        public string Complement
        {
            get => _complement;
            set => _complement = value;
        }

        public string MotherName
        {
            get => _motherName;
            set => _motherName = value;
        }

        public string FatherName
        {
            get => _fatherName;
            set => _fatherName = value;
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

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(_firstName) &&
       !string.IsNullOrEmpty(_lastName) &&
       !string.IsNullOrEmpty(_email) &&
       !string.IsNullOrEmpty(_confirmEmail) &&
       !string.IsNullOrEmpty(_phone) &&
       !string.IsNullOrEmpty(_dateBirth) &&
       !string.IsNullOrEmpty(_identifier) &&
       !string.IsNullOrEmpty(_address) &&
       !string.IsNullOrEmpty(_city) &&
       !string.IsNullOrEmpty(_number) &&
       !string.IsNullOrEmpty(_district) &&
       !string.IsNullOrEmpty(_state) &&
       !string.IsNullOrEmpty(_country) &&
       !string.IsNullOrEmpty(_zipCode) &&
       !string.IsNullOrEmpty(_complement) &&
       !string.IsNullOrEmpty(_motherName) &&
       !string.IsNullOrEmpty(_fatherName) &&
       !string.IsNullOrEmpty(_responsiblePhone) &&
       !string.IsNullOrEmpty(_responsibleEmail) &&
       !string.IsNullOrEmpty(_responsibleConfirmEmail) &&
       _email == _confirmEmail &&
       _responsibleEmail == _responsibleConfirmEmail;

        }
    }
}
