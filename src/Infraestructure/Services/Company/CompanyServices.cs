using Domain.Extensions;
using Domain.Models.Request;
using Domain.Models.Response;
using Domain.Services.Intefaces.Company;
using Infraestructure.Services.Authentication;
using Microsoft.JSInterop;
using System.Net;
using System.Text.Json;
using System.Text;
using Domain.CostumerExceptions;
using Domain.Models;

namespace Infraestructure.Services.Company
{
    public class CompanyServices : ICompanyServices, IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public CompanyServices(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        public async Task<CompanyResponseModel> CreateAsync(CompanyRequestModel companyRequestModel)
        {
            try
            {
                string token = await _jsRuntime.GetFromLocalStorage(TokenAuthenticationProvider.TokenKey);

                string json = JsonSerializer.Serialize(companyRequestModel);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                _httpClient.DefaultRequestHeaders.Authorization = new("bearer", token);
                var response = await _httpClient.PostAsync("api/company", content);

                var responseStream = await response.Content.ReadAsStringAsync();
                var responseModel = JsonSerializer.Deserialize<CompanyResponseModel>(responseStream,
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

        public async Task<Paginate<Domain.Entities.Company>> GetCompaniesAsync(int userId, int page, int pageSize)
        {
            try
            {
                string token = await _jsRuntime.GetFromLocalStorage(TokenAuthenticationProvider.TokenKey);

                _httpClient.DefaultRequestHeaders.Authorization = new("bearer", token);
                var response = await _httpClient.GetAsync($"api/company/company-by-user?page={page}&pageSize={pageSize}&userId={userId}");

                var responseStream = await response.Content.ReadAsStringAsync();

                var responseModel = JsonSerializer.Deserialize<Paginate<Domain.Entities.Company>>(responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return responseModel ?? throw new Exception("Ocorreu um erro ao obter as empresas cadastradas.");
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
