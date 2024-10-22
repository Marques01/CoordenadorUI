namespace Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }

        public int PersonId { get; set; }

        public string Login { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Salt { get; set; } = string.Empty;

        public bool Enabled { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }

        public DateTime LastLogin { get; set; }

        public int FailedCount { get; set; }

        public Person? Person { get; set; }

        public ICollection<UserRoles>? UserRoles { get; set; }
    }
}