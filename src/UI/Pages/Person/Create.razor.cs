using Domain.CostumerExceptions;
using Domain.Models.Request;
using Microsoft.JSInterop;
using UI.ViewModels;

namespace UI.Pages.Person
{
    public partial class Create
    {
        private bool
            _formIsValid = false;

        private bool
            _acceptPrivacyPolicy = false,
            _acceptPrivacyTerms = false;

        private bool
            _isLoading = false;

        private string
            _message = string.Empty;

        private PersonViewModel _personViewModel = new PersonViewModel();

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
                _isLoading = true;

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

                    var responseModel = await _personServices.CreatePersonAsync(personRequestModel);

                    await _jsRuntime.InvokeVoidAsync("showModal");
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
                _isLoading = false;
            }
        }

        private string ConvertErrorsToString(IEnumerable<string> errorMessages)
        {
            return string.Join(Environment.NewLine, errorMessages);
        }

        private void FormIsValid()
        {
            _formIsValid = _personViewModel.IsValid();
        }

        private void Refresh() => _navigationManager.NavigateTo("/");
    }
}
