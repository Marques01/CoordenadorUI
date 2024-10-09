using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UI.ViewModels
{
    public class AccountViewModel
    {
        private string
            _email = string.Empty,
            _password = string.Empty;


        [Required(AllowEmptyStrings = false)]
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

        [Required(AllowEmptyStrings = false)]
        public string Password
        {
            get => _password;
            set => _password = value.Trim();
        }

        public bool RememberMe { get; set; }
    }
}
