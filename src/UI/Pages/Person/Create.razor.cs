using UI.ViewModels;

namespace UI.Pages.Person
{
    public partial class Create
    {
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
    }
}
