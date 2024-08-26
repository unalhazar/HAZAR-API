using Application.Base;

namespace Application.Features.Brands.Requests
{
    public class BrandRequest : BaseRequest
    {
        public required string Name { get; set; }

    }
}
