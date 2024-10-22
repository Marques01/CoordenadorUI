using System.Text.RegularExpressions;

namespace Domain.Models.Request
{
    public class AddressRequestModel
    {
        private string
            _zipCode = string.Empty;

        public string ZipCode
        {
            get => _zipCode;
            set => _zipCode = Regex.Replace(value, @"[^0-9]", string.Empty).Trim();
        }
    }
}
