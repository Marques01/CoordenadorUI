using Domain.CostumerExceptions;
using Domain.Entities;
using Domain.Models.Request;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace UI.Pages.User;

public partial class Create
{
    private string
        _passwordInputType = "password",
        _passwordIcon = "bi-eye",
        _message = string.Empty;

    private bool
        _isLoading = false,
        _isSpinning = false;

    private string
        _email = string.Empty,
        _password = string.Empty,
        _confirmPassword = string.Empty;

    private string
        _roleId = string.Empty;

    private ElementReference _elementReference;

    private IEnumerable<Roles> _roles = Enumerable.Empty<Roles>();

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

    protected override async Task OnInitializedAsync()
    {
        await GetRolesAsync();
    }

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
                _email = responseModel.Model!.Email;
            }
        }
        catch (ArgumentException arg)
        {
            Console.WriteLine(arg.Message);
            await _jsRuntime.InvokeVoidAsync("showFailureSearchModal");
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

    private async Task OnSubmitAsync()
    {
        try
        {
            bool formIsValid = FormIsValid();

            if (formIsValid)
            {
                _isSpinning = true;

                await Task.Delay(2000);

                var responseModel = await _accountServices.SignUpAsync(new UserRequestModel
                {
                    Login = _email,
                    Password = _password,
                });

                UserRoles userRoles = new()
                {
                    RoleId = Guid.Parse(_roleId),
                    UserId = responseModel.Model!.UserId
                };

                await _accountServices.IncludeUserRoleAsync(userRoles);

                await _jsRuntime.InvokeVoidAsync("showSuccessModal", responseModel.Message);
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

    private bool FormIsValid()
    {
        bool isValid = !string.IsNullOrEmpty(_email) && _email.Contains("@") && _email.Contains(".com")
            && !string.IsNullOrEmpty(_password) && !string.IsNullOrEmpty(_confirmPassword) && _password == _confirmPassword
            && !string.IsNullOrEmpty(_roleId);

        return isValid;
    }

    private string ConvertErrorsToString(IEnumerable<string> errorMessages)
    {
        return string.Join(Environment.NewLine, errorMessages);
    }

    private void OnProfileChanged(ChangeEventArgs e)
    {
        string value = Convert.ToString(e.Value) ?? string.Empty;

        if (!string.IsNullOrEmpty(value))
        {
            _roleId = value;
        }
    }
}