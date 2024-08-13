using Microsoft.Extensions.Hosting;

namespace Infrastructure.AppServices.Background
{
    public class PeriodicTaskService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Yapmak istediğiniz periyodik işlemleri buraya ekleyin
                Console.WriteLine("Periyodik görev çalışıyor.");
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}
