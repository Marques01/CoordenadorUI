using Domain.Models.Response;

namespace Domain.Services.Intefaces.Adress.Interfaces
{
    public interface IAdressLocatorServices
    {
        Task<AdressResponseModel> GetAdressByZipCodeAsync(string zipCode);
    }
}
