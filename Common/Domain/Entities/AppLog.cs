using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class AppLog
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public string MessageTemplate { get; set; }
        public string Level { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Exception { get; set; }
        public string LogEvent { get; set; } // JSONB olarak saklanacak
    }
}
