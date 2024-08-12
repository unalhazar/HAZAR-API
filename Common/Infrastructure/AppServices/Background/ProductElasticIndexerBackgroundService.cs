using Infrastructure.ElasticSearch;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.AppServices.Background
{
    public class ProductElasticIndexerBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<ProductElasticIndexerBackgroundService> _logger;

        public ProductElasticIndexerBackgroundService(IServiceScopeFactory serviceScopeFactory, ILogger<ProductElasticIndexerBackgroundService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ProductElasticIndexerBackgroundService started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var productElasticIndexer = scope.ServiceProvider.GetRequiredService<ProductElasticIndexer>();

                        _logger.LogInformation("Starting ElasticSearch indexing.");
                        await productElasticIndexer.IndexProductsAsync();
                        _logger.LogInformation("ElasticSearch indexing completed.");
                    }

                    // 3 dakika bekle ve tekrar çalıştır (örnek olarak 3 dakika seçildi)
                    await Task.Delay(TimeSpan.FromMinutes(3), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while indexing products.");
                }
            }

            _logger.LogInformation("ProductElasticIndexerBackgroundService stopped.");
        }
    }
}
