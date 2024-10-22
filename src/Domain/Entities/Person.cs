namespace Domain.Entities
{
    public class Person
    {
        public int PersonId { get; set; }

        public string Identifier { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public DateTime DateBirth { get; set; }

        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Number { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string ZipCode { get; set; } = string.Empty;

        public string District { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string Complement { get; set; } = string.Empty;

        public string MotherName { get; set; } = string.Empty;

        public string FatherName { get; set; } = string.Empty;

        public string ResponsiblePhone { get; set; } = string.Empty;

        public string ResponsibleEmail { get; set; } = string.Empty;

        public bool Enabled { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}
