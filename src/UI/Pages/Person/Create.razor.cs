using Domain.CostumerExceptions;
using Domain.Entities;
using Domain.Models;
using Domain.Models.Request;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using UI.ViewModels;

namespace UI.Pages.Person
{
    public partial class Create : ComponentBase
    {
        private bool
            _formIsValid = false,
            _acceptPrivacyPolicy = false,
            _acceptPrivacyTerms = false,
            _isLoading = false,
            _isSpinning = false,
            _pageAllowed = false;

        private string
            _message = string.Empty,
            _passwordInputType = "password",
            _passwordIcon = "bi-eye",
            _selectValue = string.Empty,
            _password = string.Empty,
            _confirmPassword = string.Empty,
            _roleId = string.Empty;

        private int
            _userId = default,
            _companyId = default;

        private readonly List<string> _loadingMessages = new()
        {
            "Carregando... Por favor, aguarde.",
            "Estamos preparando tudo para você...",
            "Um momento, estamos quase lá...",
            "Só mais um pouquinho...",
            "Estamos trabalhando nisso...",
            "Aguarde enquanto fazemos a mágica acontecer...",
            "Estamos colocando as coisas em ordem para você...",
            "Estamos a todo vapor por aqui...",
            "Estamos dando os retoques finais...",
            "Estamos polindo as últimas peças...",
            "Estamos quase prontos para você...",
            "Estamos carregando sua experiência...",
            "Estamos preparando o palco...",
            "Estamos afinando as coisas para você...",
            "Estamos carregando os últimos detalhes..."
        };

        private PersonViewModel _personViewModel = new PersonViewModel();
        private Paginate<Domain.Entities.Company> _companyPaginated = new();
        private List<Domain.Entities.Company> _companiesToUserAccess = new();
        private List<Domain.Entities.Company> _companies = new();
        private IEnumerable<Roles> _roles = Enumerable.Empty<Roles>();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                _isLoading = true;

                var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;

                _pageAllowed = user.IsInRole("Administrator") || user.IsInRole("Developer");
                if (!_pageAllowed)
                {
                    _navigationManager.NavigateTo("/user-not-authorized");
                    return;
                }

                var claims = user.Claims.ToList();

                if (claims.Count > 2)
                {
                    var idUserClaim = claims[2].Value;
                    _userId = int.Parse(idUserClaim);
                }

                await GetCompaniesPaginatedAsync();
                await GetRolesAsync();

                _isLoading = false;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && _pageAllowed)
            {
                await _jsRuntime.InvokeVoidAsync("import", "./js/person/create.js");
            }
        }

        private async Task SearchAdressAsync()
        {
            try
            {
                var responseModel = await _adressLocatorServices.GetAdressByZipCodeAsync(_personViewModel.ZipCode);

                if (responseModel.IsSuccess && responseModel.Model is not null)
                {
                    _personViewModel.Address = responseModel.Model.Address;
                    _personViewModel.City = responseModel.Model.City;
                    _personViewModel.District = responseModel.Model.District;
                    _personViewModel.State = responseModel.Model.State;
                    _personViewModel.Country = "Brasil";

                    ValidateInputAddress();
                    ValidateInputCity();
                    ValidateInputDistrict();
                    ValidateInputState();

                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async Task OnSubmitAsync()
        {
            try
            {
                _isSpinning = true;

                FormIsValid();

                if (_formIsValid && _acceptPrivacyPolicy && _acceptPrivacyTerms)
                {
                    PersonRequestModel personRequestModel = new PersonRequestModel
                    {
                        Identifier = _personViewModel.Identifier.Trim(),
                        FirstName = _personViewModel.FirstName.Trim(),
                        LastName = _personViewModel.LastName.Trim(),
                        Email = _personViewModel.Email.Trim(),
                        Phone = _personViewModel.Phone.Trim(),
                        DateBirth = Convert.ToDateTime(_personViewModel.DateBirth),
                        Address = _personViewModel.Address.Trim(),
                        City = _personViewModel.City.Trim(),
                        Number = _personViewModel.Number.Trim(),
                        State = _personViewModel.State.Trim(),
                        ZipCode = _personViewModel.ZipCode.Trim(),
                        District = _personViewModel.District.Trim(),
                        Country = _personViewModel.Country.Trim(),
                        Complement = _personViewModel.Complement.Trim(),
                        MotherName = _personViewModel.MotherName.Trim(),
                        FatherName = _personViewModel.FatherName.Trim(),
                        ResponsiblePhone = _personViewModel.ResponsiblePhone.Trim(),
                        ResponsibleEmail = _personViewModel.ResponsibleEmail.Trim(),
                    };

                    var personResponseModel = await _personServices.CreatePersonAsync(personRequestModel);

                    var userRequestModel = new UserRequestModel()
                    {
                        Login = personRequestModel.Email,
                        Password = _password
                    };

                    var accountResponseModel = await _accountServices.SignUpAsync(userRequestModel);
                    var user = accountResponseModel.Model!;

                    UserRoles userRoles = new()
                    {
                        RoleId = Guid.Parse(_roleId),
                        UserId = user.UserId,
                    };

                    await _accountServices.IncludeUserRoleAsync(userRoles);

                    UserCompanyRequestModel userCompanyRequestModel = new UserCompanyRequestModel
                    {
                        UserId = user.UserId,
                    };

                    foreach (var userCompanyAcess in _companiesToUserAccess)
                    {
                        userCompanyRequestModel.CompanyId = userCompanyAcess.CompanyId;
                        await _companyServices.AssociateUserCompanyAsync(userCompanyRequestModel);
                    }

                    await _jsRuntime.InvokeVoidAsync("showModal");
                    await ClearInputs();
                }
            }
            catch (ArgumentException arg)
            {
                _message = arg.Message;
                await _jsRuntime.InvokeVoidAsync("showFailureModal");
            }
            catch (ValidationException val)
            {
                _message = ConvertErrorsToString(val.ErrorMessages);
                await _jsRuntime.InvokeVoidAsync("showFailureModal");
            }
            catch (Exception ex)
            {
                _message = ex.Message;
                await _jsRuntime.InvokeVoidAsync("showFailureModal");
            }
            finally
            {
                _isSpinning = false;
            }
        }

        private async Task GetRolesAsync()
        {
            try
            {
                _roles = await _rolesServices.GetRolesAsync();
            }
            catch (ArgumentException arg)
            {
                _message = arg.Message;
                await _jsRuntime.InvokeVoidAsync("showFailureModal");
            }
            catch (ValidationException val)
            {
                _message = ConvertErrorsToString(val.ErrorMessages);
                await _jsRuntime.InvokeVoidAsync("showFailureModal");
            }
            catch (Exception ex)
            {
                _message = ex.Message;
                await _jsRuntime.InvokeVoidAsync("showFailureModal");
            }
        }

        private async Task GetCompaniesPaginatedAsync()
        {
            try
            {
                var responseModel = await _companyServices.GetCompaniesAsync(_userId, 1, int.MaxValue);
                _companies = responseModel.Items.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private string ConvertErrorsToString(IEnumerable<string> errorMessages)
        {
            return string.Join(Environment.NewLine, errorMessages);
        }

        private void FormIsValid()
        {
            bool passwordIsValid = !string.IsNullOrEmpty(_password) && !string.IsNullOrEmpty(_confirmPassword) &&
                                   _password == _confirmPassword;

            bool roleIsValid = !string.IsNullOrEmpty(Convert.ToString(_roleId));

            _formIsValid = _personViewModel.IsValid() && passwordIsValid && roleIsValid;
        }

        private void OnCompanyChanged(ChangeEventArgs e)
        {
            string value = !string.IsNullOrEmpty(_selectValue) ? _selectValue : "0";
            int newId = Convert.ToInt32(value);

            if (newId > 0)
                _companyId = newId;
        }

        private void AddToUserCompany()
        {
            if (_companyId > 0)
            {
                var company = _companies.FirstOrDefault(x => x.CompanyId == _companyId);
                if (company is not null)
                {
                    _companiesToUserAccess.Add(company);
                    _companies.Remove(company);
                    _selectValue = string.Empty;
                }
            }
        }

        private void RemoveFromUserCompany(Domain.Entities.Company company)
        {
            _companiesToUserAccess.Remove(company);
            _companies.Add(company);
            _selectValue = string.Empty;
        }

        private string GetRandomLoadingMessage()
        {
            var random = new Random();
            int index = random.Next(_loadingMessages.Count);
            return _loadingMessages[index];
        }

        private void TogglePasswordVisibility()
        {
            _passwordInputType = _passwordInputType == "password" ? "text" : "password";
            _passwordIcon = _passwordIcon == "bi-eye" ? "bi-eye-slash" : "bi-eye";
        }

        private void Refresh() => _navigationManager.NavigateTo("/");
    }
}
