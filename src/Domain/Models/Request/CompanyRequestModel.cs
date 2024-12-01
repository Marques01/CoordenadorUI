using Domain.Extensions;
using System.Text.RegularExpressions;

namespace Domain.Models.Request
{
    public class CompanyRequestModel
    {
        private string
            _name = string.Empty,
            _coorporateTaxPayerRoot = string.Empty,
            _coorporateTaxPayer = string.Empty,
            _phone = string.Empty,
            _address = string.Empty,
            _city = string.Empty,
            _number = string.Empty,
            _district = string.Empty,
            _state = string.Empty,
            _zipCode = string.Empty;

        public string Name
        {
            get => _name;
            set => _name = value.CleanInput();
        }

        public string CoorporateTaxPayerRoot
        {
            get => _coorporateTaxPayerRoot;
            set
            {
                string cleanedValue = Regex.Replace(value, @"[^0-9]", string.Empty);
                _coorporateTaxPayerRoot = cleanedValue;
            }
        }

        public string CoorporateTaxPayer
        {
            get => _coorporateTaxPayer;
            set
            {
                string cleanedValue = Regex.Replace(value, @"[^0-9]", string.Empty);
                _coorporateTaxPayer = cleanedValue;
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                string cleanedValue = Regex.Replace(value, @"[^0-9]", string.Empty);
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

        public string ZipCode
        {
            get => _zipCode;
            set => _zipCode = Regex.Replace(value, @"[^0-9]", string.Empty);
        }
    }
}
