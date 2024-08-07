namespace Domain.Entities
{
    public class LogUser
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public LogUserLevel? LogLevel { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Operation { get; set; }
        public string? UserId { get; set; }
    }
}
