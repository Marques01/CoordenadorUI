using Domain.CostumerExceptions;
using Domain.Models.Request;
using Domain.Models.Response;
using Domain.Services.Intefaces.Account;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Domain.Extensions;
using Infraestructure.Services.Authentication;
using Microsoft.JSInterop;
using ArgumentException = System.ArgumentException;

namespace Infraestructure.Services.Account
{
    public class AccountServices : IAccountServices, IDisposable
    {
        private readonly HttpClient _httpClient;

        private readonly IJSRuntime _jsRuntime;
        public AccountServices(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
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

        public async Task<UserTokenResponseModel> RefreshAsync(string token)
        {
            try
            {
                string refreshToken = await _jsRuntime.GetFromLocalStorage(TokenAuthenticationProvider.RefreshTokenKey);

                var userTokenRequestModel = new UserTokenRequestModel()
                {
                    Token = token,
                    RefreshToken = refreshToken
                };

                string json = JsonSerializer.Serialize(userTokenRequestModel);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                var authenticationHeaderValue = new AuthenticationHeaderValue("bearer", token);
                _httpClient.DefaultRequestHeaders.Authorization = authenticationHeaderValue;
                _httpClient.DefaultRequestHeaders.Add("RefreshToken", refreshToken);
                var response = await _httpClient.PostAsync("api/account/refresh", content);

                var responseStrings = await response.Content.ReadAsStringAsync();
                var responseModel = JsonSerializer.Deserialize<UserTokenResponseModel>(responseStrings,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                if (responseModel is null)
                    throw new Exception("Failed to deserialize response");

                if (responseModel.StatusCode == HttpStatusCode.BadRequest)
                    throw new ArgumentException(responseModel.Message.First());

                if (responseModel.StatusCode == HttpStatusCode.Unauthorized)
                    throw new ArgumentException(responseModel.Message.First());

                if (responseModel.StatusCode == HttpStatusCode.InternalServerError)
                    throw new Exception(responseModel.Message.First());

                await _jsRuntime.RemoveItem(TokenAuthenticationProvider.TokenKey);
                await _jsRuntime.RemoveItem(TokenAuthenticationProvider.RefreshTokenKey);

                await Task.Delay(500);

                await _jsRuntime.SetInLocalStorage(TokenAuthenticationProvider.TokenKey, responseModel.Model!.Token);
                await _jsRuntime.SetInLocalStorage(TokenAuthenticationProvider.RefreshTokenKey, responseModel.Model!.RefreshToken);

                return responseModel;
            }
            catch (ArgumentException arg)
            {
                throw new ArgumentException(arg.Message);
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
