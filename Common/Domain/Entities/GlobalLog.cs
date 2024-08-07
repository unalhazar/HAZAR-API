using Microsoft.Extensions.Logging;

namespace Domain.Entities
{
    public class GlobalLog
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Operation { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public LogLevel LogLevel { get; set; }
    }
}
