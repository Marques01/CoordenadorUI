using Domain.CostumerExceptions;
using Domain.Extensions;
using Domain.Models.Request;
using Domain.Models.Response;
using Domain.Services.Intefaces.Person;
using Infraestructure.Services.Authentication;
using Microsoft.JSInterop;
using System.Net;
using System.Text;
using System.Text.Json;
using Domain.Models;

namespace Infraestructure.Services.Person
{
    public class PersonServices : IPersonServices, IDisposable
    {
        private readonly HttpClient _httpClient;

        private readonly IJSRuntime _jsRuntime;

        public PersonServices(HttpClient httpClient, IJSRuntime jSRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jSRuntime;
        }

        public async Task<PersonResponseModel> CreatePersonAsync(PersonRequestModel personRequestModel)
        {
            try
            {
                string token = await _jsRuntime.GetFromLocalStorage(TokenAuthenticationProvider.TokenKey);

                string json = JsonSerializer.Serialize(personRequestModel);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                _httpClient.DefaultRequestHeaders.Authorization = new("bearer", token);
                var response = await _httpClient.PostAsync("api/person", content);

                var responseStream = await response.Content.ReadAsStringAsync();
                var responseModel = JsonSerializer.Deserialize<PersonResponseModel>(responseStream,
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

        public async Task<Paginate<Domain.Entities.Person>> GetPersonsAsync(int page, int pageSize)
        {
            try
            {
                string token = await _jsRuntime.GetFromLocalStorage(TokenAuthenticationProvider.TokenKey);

                _httpClient.DefaultRequestHeaders.Authorization = new("bearer", token);
                var response = await _httpClient.GetAsync($"api/person?page={page}&pageSize={pageSize}");

                var responseStream = await response.Content.ReadAsStringAsync();

                var responseModel = JsonSerializer.Deserialize<Paginate<Domain.Entities.Person>>(responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return responseModel ?? throw new Exception("Ocorreu um erro ao obter as pessoas cadastradas.");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Paginate<Domain.Entities.Person>> GetPersonsByCompanyIdAsync(int companyId, int page, int pageSize)
        {
            try
            {
                string token = await _jsRuntime.GetFromLocalStorage(TokenAuthenticationProvider.TokenKey);

                _httpClient.DefaultRequestHeaders.Authorization = new("bearer", token);
                var response = await _httpClient.GetAsync($"api/person/usercompany?page={page}&pageSize={pageSize}&companyId={companyId}");

                var responseStream = await response.Content.ReadAsStringAsync();

                var responseModel = JsonSerializer.Deserialize<Paginate<Domain.Entities.Person>>(responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return responseModel ?? throw new Exception("Ocorreu um erro ao obter as pessoas cadastradas.");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<PersonResponseModel> GetByEmailAsync(string email)
        {
            try
            {
                string token = await _jsRuntime.GetFromLocalStorage(TokenAuthenticationProvider.TokenKey);

                _httpClient.DefaultRequestHeaders.Authorization = new("bearer", token);
                var response = await _httpClient.GetAsync($"api/person/{email}");

                var responseStream = await response.Content.ReadAsStringAsync();
                var responseModel = JsonSerializer.Deserialize<PersonResponseModel>(responseStream,
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
