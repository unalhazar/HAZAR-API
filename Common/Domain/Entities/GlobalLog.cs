using Microsoft.Extensions.Logging;

namespace Domain.Entities
{
    public class GlobalLog
    {
        public GlobalLog(int ıd, string message, string operation, string? userId, DateTime createdDate, LogLevel logLevel)
        {
            Id = ıd;
            Message = message;
            Operation = operation;
            UserId = userId;
            CreatedDate = createdDate;
            LogLevel = logLevel;
        }

        public GlobalLog()
        {
            throw new NotImplementedException();
        }

        public int Id { get; set; }
        public string Message { get; set; }
        public string Operation { get; set; }
        public string? UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public LogLevel LogLevel { get; set; }
    }
}
