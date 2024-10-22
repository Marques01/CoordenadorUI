using Domain.CostumerExceptions;
using Domain.Models.Request;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using UI.ViewModels;

namespace UI.Pages.Account
{
    public partial class SignIn
    {
        private bool
            _isLoading = false;

        private readonly AccountViewModel _accountViewModel = new AccountViewModel();

        private EditContext? _editContext;

        private ElementReference _elementReference;

        private IEnumerable<string> _errorMessages = Enumerable.Empty<string>();

        protected override void OnInitialized()
        {
            _editContext = new EditContext(_accountViewModel);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await _elementReference.FocusAsync();
            }
        }

        private async Task SubmitAsync()
        {
            try
            {
                ClearMessages();

                if (_editContext is not null && _editContext.Validate())
                {
                    _isLoading = true;

                    var userRequestModel = new UserRequestModel()
                    {
                        Login = _accountViewModel.Email,
                        Password = _accountViewModel.Password,
                        RememberMe = _accountViewModel.RememberMe
                    };

                    var responseModel = await _accountServices.SignInAsync(userRequestModel);
                    var userToken = responseModel.Model;

                    if (userToken is not null)
                    {
                        if (!string.IsNullOrEmpty(userToken.RefreshToken))
                        {
                            await _authorizeServices.LoginAsync(userToken.Token, userToken.RefreshToken);
                            return;
                        }

                        await _authorizeServices.LoginAsync(userToken.Token);
                    }

                    // Redirect to another page
                    _navigationManager.NavigateTo("/");
                }
            }
            catch (ValidationException val)
            {
                _errorMessages = val.ErrorMessages;
            }
            catch (ArgumentException arg)
            {
                _errorMessages = new List<string>() { arg.Message };
            }
            catch (Exception e)
            {
                _errorMessages = new List<string>() { e.Message };
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void ClearMessages()
        {
            _errorMessages = Enumerable.Empty<string>();
        }
    }
}
