namespace Domain.Entities
{
    public class Company
    {
        public int CompanyId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string CoorporateTaxPayerRoot { get; set; } = string.Empty;

        public string CoorporateTaxPayer { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Number { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string ZipCode { get; set; } = string.Empty;

        public string District { get; set; } = string.Empty;

        public bool Enabled { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
