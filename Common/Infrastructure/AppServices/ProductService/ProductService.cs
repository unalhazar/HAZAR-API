using Application.Abstraction;
using OfficeOpenXml;

namespace Infrastructure.AppServices.ProductService
{
    public class ProductService(IProductRepository productRepository) : IProductService
    {
        public async Task ImportProductsAsync(string filePath)
        {
            var products = new List<Product>();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets.First();
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    products.Add(new Product
                    {
                        Name = worksheet.Cells[row, 1].Text,
                        Description = worksheet.Cells[row, 2].Text,
                        Price = int.Parse(worksheet.Cells[row, 3].Text),
                        Stock = int.Parse(worksheet.Cells[row, 4].Text),
                        ImageUrl = worksheet.Cells[row, 5].Text,
                        CategoryId = long.Parse(worksheet.Cells[row, 6].Text),
                        BrandId = long.Parse(worksheet.Cells[row, 7].Text)
                    });
                }
            }

            await productRepository.BulkAddAsync(products);
        }
    }
}
