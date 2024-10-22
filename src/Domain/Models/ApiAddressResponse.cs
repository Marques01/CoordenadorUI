using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class ApiAddressResponse
    {
        [JsonPropertyName("cep")]
        public string ZipCode { get; set; } = string.Empty;

        [JsonPropertyName("logradouro")]
        public string Address { get; set; } = string.Empty;

        [JsonPropertyName("complemento")]
        public string Complement { get; set; } = string.Empty;

        [JsonPropertyName("bairro")]
        public string District { get; set; } = string.Empty;

        [JsonPropertyName("localidade")]
        public string City { get; set; } = string.Empty;

        [JsonPropertyName("uf")]
        public string State { get; set; } = string.Empty;

        [JsonPropertyName("estado")]
        public string FullStateName { get; set; } = string.Empty;

    }
}
