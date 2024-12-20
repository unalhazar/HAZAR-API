﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class AppLog
    {
        // Parametresiz constructor
        public AppLog() { }

        // Parametreli constructor
        public AppLog(
            int id,
            string message,
            string messageTemplate,
            string level,
            DateTime timeStamp,
            string exception,
            string logEvent)
        {
            Id = id;
            Message = message;
            MessageTemplate = messageTemplate;
            Level = level;
            TimeStamp = timeStamp;
            Exception = exception;
            LogEvent = logEvent;
        }

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