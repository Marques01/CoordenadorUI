namespace Domain.Entities
{
    public class UserCompany
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int CompanyId { get; set; }

        public DateTime CreateAt { get; set; }

        public bool Enabled { get; set; }

        public User? User { get; set; }

        public Company? Company { get; set; }
    }
}
