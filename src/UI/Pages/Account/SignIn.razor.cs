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
            if (_editContext is not null && _editContext.Validate())
            {
                _isLoading = true;

                // Do something with the validated data
                await Task.Delay(4000);

                // Redirect to another page

                _isLoading = false;
                return;
            }
        }
    }
}
