using Domain.CostumerExceptions;
using Domain.Models.Request;
using Microsoft.JSInterop;
using UI.ViewModels;

namespace UI.Pages.Company;

public partial class Create
{
    private string _message = string.Empty;
    private bool _isSpinning = false, _isLoading = false, _pageAllowed = false;
    private CompanyViewModel _companyViewModel = new();

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
            await _jsRuntime.InvokeVoidAsync("import", "./js/company/create.js");
        }
    }

    private async Task OnSubmitAsync()
    {
        try
        {
            _isSpinning = true;

            if (!FormIsValid())
                throw new ArgumentException("Por favor, preencha todos os campos.");

            CompanyRequestModel companyRequestModel = new()
            {
                Address = _companyViewModel.Address,
                City = _companyViewModel.City,
                CoorporateTaxPayer = _companyViewModel.CoorporateTaxPayer,
                CoorporateTaxPayerRoot = _companyViewModel.CoorporateTaxPayerRoot,
                District = _companyViewModel.District,
                Name = _companyViewModel.Name,
                Number = _companyViewModel.Number,
                Phone = _companyViewModel.Phone,
                State = _companyViewModel.State,
                ZipCode = _companyViewModel.ZipCode
            };

            var responseModel = await _companyServices.CreateAsync(companyRequestModel);
            await _jsRuntime.InvokeVoidAsync("showCompanyModal");
        }
        catch (ArgumentException arg)
        {
            _message = arg.Message;
            await _jsRuntime.InvokeVoidAsync("showFailureCompanyModal", _message);
        }
        catch (ValidationException val)
        {
            _message = ConvertErrorsToString(val.ErrorMessages);
            await _jsRuntime.InvokeVoidAsync("showFailureCompanyModal", _message);
        }
        catch (Exception ex)
        {
            _message = ex.Message;
            await _jsRuntime.InvokeVoidAsync("showFailureCompanyModal", _message);
        }
        finally
        {
            _isSpinning = false;
        }
    }

    private async Task SearchAdressAsync()
    {
        try
        {
            var responseModel = await _adressLocatorServices.GetAdressByZipCodeAsync(_companyViewModel.ZipCode);

            if (responseModel.IsSuccess && responseModel.Model is not null)
            {
                _companyViewModel.Address = responseModel.Model.Address;
                _companyViewModel.City = responseModel.Model.City;
                _companyViewModel.District = responseModel.Model.District;
                _companyViewModel.State = responseModel.Model.State;

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

    private string GetRandomLoadingMessage()
    {
        var random = new Random();
        int index = random.Next(_loadingMessages.Count);
        return _loadingMessages[index];
    }

    private string ConvertErrorsToString(IEnumerable<string> errorMessages)
    {
        return string.Join(Environment.NewLine, errorMessages);
    }

    private bool FormIsValid()
    {
        return _companyViewModel.IsValid();
    }
}
