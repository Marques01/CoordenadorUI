using Domain.CostumerExceptions;
using Domain.Models.Request;
using Microsoft.JSInterop;
using UI.ViewModels;

namespace UI.Pages.Company;

public partial class Create
{
    private string
        _message = string.Empty;

    private bool
        _isSpinning = false;

    private CompanyViewModel _companyViewModel = new();

    private async Task OnSubmitAsync()
    {
        try
        {
            // Implment create
            _isSpinning = true;

            if (!FormIsValid())
                throw new ArgumentException("Por favor, preencha todos os campos.");

            // Implement create

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
            await _jsRuntime.InvokeVoidAsync("showModal");
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

    private string ConvertErrorsToString(IEnumerable<string> errorMessages)
    {
        return string.Join(Environment.NewLine, errorMessages);
    }

    private bool FormIsValid()
    {
        return _companyViewModel.IsValid();
    }
}