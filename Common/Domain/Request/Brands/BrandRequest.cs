namespace Domain.Request.Brands
{
    public class BrandRequest : BaseRequest
    {
        public required string Name { get; set; }
        public int? State { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? CreatedUserId { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? UpdatedUserId { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
