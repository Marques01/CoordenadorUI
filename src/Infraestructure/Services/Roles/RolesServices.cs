using Domain.Extensions;
using Domain.Services.Intefaces.Roles;
using Infraestructure.Services.Authentication;
using Microsoft.JSInterop;
using System.Text.Json;

namespace Infraestructure.Services.Roles
{
    public class RolesServices : IRolesServices, IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public RolesServices(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        public async Task<IEnumerable<Domain.Entities.Roles>> GetRolesAsync()
        {
            try
            {
                string token = await _jsRuntime.GetFromLocalStorage(TokenAuthenticationProvider.TokenKey);

                _httpClient.DefaultRequestHeaders.Authorization = new("bearer", token);
                var response = await _httpClient.GetAsync("api/roles");

                var responseStream = await response.Content.ReadAsStringAsync();
                var responseModel = JsonSerializer.Deserialize<IEnumerable<Domain.Entities.Roles>>(responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return responseModel ?? Enumerable.Empty<Domain.Entities.Roles>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("Ocorreu um error ao buscar os perfis.");
            }
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
