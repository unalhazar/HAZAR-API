using Domain.Base;

namespace Domain.Request.Brands
{
    public class BrandRequest : BaseRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
