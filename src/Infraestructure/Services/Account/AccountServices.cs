using System.Net;
using System.Text;
using System.Text.Json;
using Domain.CostumerExceptions;
using Domain.Models.Request;
using Domain.Models.Response;
using Domain.Services.Intefaces.Account;
using ArgumentException = System.ArgumentException;

namespace Infraestructure.Services.Account
{
    public class AccountServices : IAccountServices, IDisposable
    {
        private readonly HttpClient _httpClient;

        public AccountServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserTokenResponseModel> SignInAsync(UserRequestModel requestModel)
        {
            try
            {
                string json = JsonSerializer.Serialize(requestModel);

                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/account/signin", content);

                var responseStream = await response.Content.ReadAsStringAsync();
                var responseModel = JsonSerializer.Deserialize<UserTokenResponseModel>(responseStream,
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
            catch (ValidationException val)
            {
                throw new ValidationException(val.ErrorMessages);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
