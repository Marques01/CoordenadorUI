using Domain.Models.Response;
using Domain.Services.Intefaces.Adress.Interfaces;
using System.Net;
using System.Text.Json;
using Domain.CostumerExceptions;

namespace Infraestructure.Services.Adress
{
    public class AdressLocatorServices : IAdressLocatorServices, IDisposable
    {
        private readonly HttpClient _httpClient;

        public AdressLocatorServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AdressResponseModel> GetAdressByZipCodeAsync(string zipCode)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/adress?zipcode={zipCode}");

                var responseStream = await response.Content.ReadAsStringAsync();
                var responseModel = JsonSerializer.Deserialize<AdressResponseModel>(responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                if (responseModel is null)
                    throw new Exception("Failed to deserialize response");

                if (responseModel.StatusCode == HttpStatusCode.BadRequest && responseModel.Message.Count() > 1)
                    throw new ValidationException(responseModel.Message);

                if (responseModel.StatusCode == HttpStatusCode.BadRequest && responseModel.Message.Count() == 1)
                    throw new ArgumentException(responseModel.Message.First());

                if (responseModel.StatusCode == HttpStatusCode.InternalServerError)
                    throw new Exception(responseModel.Message.First());

                return responseModel;
            }
            catch (ArgumentException arg)
            {
                throw new ArgumentException(arg.Message);
            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
