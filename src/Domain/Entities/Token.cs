namespace Domain.Entities
{
    public class Token
    {
        public int TokenId { get; set; }

        public Guid Key { get; set; }

        public DateTime Validity { get; set; }
    }
}
