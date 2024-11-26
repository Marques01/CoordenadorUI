using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace UI.Pages.User;

public partial class Create
{
    private string
        _passwordInputType = "password",
        _passwordIcon = "bi-eye";

    private ElementReference _elementReference;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await _elementReference.FocusAsync();
        }
    }

    private async Task OnInputEmailChanged(ChangeEventArgs e)
    {
        try
        {
            string value = Convert.ToString(e.Value) ?? string.Empty;

            if (!string.IsNullOrEmpty(value))
            {
                await _jsRuntime.InvokeVoidAsync("showModal");

                await Task.Delay(2000);

                var responseModel = await _personServices.GetByEmailAsync(value);
            }
        }
        catch (ArgumentException arg)
        {
            Console.WriteLine(arg.Message);
            await _jsRuntime.InvokeVoidAsync("showFailureSearchModal");
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        finally
        {
            await _jsRuntime.InvokeVoidAsync("closeModal");
        }
    }

    private void TogglePasswordVisibility()
    {
        _passwordInputType = _passwordInputType == "password" ? "text" : "password";
        _passwordIcon = _passwordIcon == "bi-eye" ? "bi-eye-slash" : "bi-eye";
    }
}