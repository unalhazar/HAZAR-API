using Infrastructure.ElasticSearch;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.AppServices.Background
{
    public class ProductElasticIndexerBackgroundService(
        IServiceScopeFactory serviceScopeFactory,
        ILogger<ProductElasticIndexerBackgroundService> logger)
        : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("ProductElasticIndexerBackgroundService started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = serviceScopeFactory.CreateScope())
                    {
                        var productElasticIndexer = scope.ServiceProvider.GetRequiredService<ProductElasticIndexer>();

                        logger.LogInformation("Starting ElasticSearch indexing.");
                        await productElasticIndexer.IndexProductsAsync();
                        logger.LogInformation("ElasticSearch indexing completed.");
                    }

                    // 3 dakika bekle ve tekrar çalıştır (örnek olarak 3 dakika seçildi)
                    await Task.Delay(TimeSpan.FromMinutes(3), stoppingToken);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while indexing products.");
                }
            }

            logger.LogInformation("ProductElasticIndexerBackgroundService stopped.");
        }
    }
}
