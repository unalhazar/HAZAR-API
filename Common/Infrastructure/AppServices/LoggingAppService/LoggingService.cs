using Microsoft.Extensions.Configuration;
using NpgsqlTypes;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;

namespace Infrastructure.AppServices.LoggingAppService
{
    public static class LoggingService
    {
        public static void ConfigureLogging(IConfiguration configuration)
        {
            var columnWriters = new Dictionary<string, ColumnWriterBase>
        {
            { "message", new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
            { "message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text) },
            { "level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
            { "time_stamp", new TimestampColumnWriter(NpgsqlDbType.Timestamp) },
            { "exception", new ExceptionColumnWriter(NpgsqlDbType.Text) },
            { "log_event", new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb) }
        };

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.PostgreSQL(
                    connectionString: configuration.GetConnectionString("Default"),
                    tableName: "Logs",
                    columnOptions: columnWriters,
                    needAutoCreateTable: true)
                .CreateLogger();
        }
    }
}
