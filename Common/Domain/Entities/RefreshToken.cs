using Domain.Base;

namespace Domain.Entities
{
    public class RefreshToken : BaseEntity
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public long UserId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
        public ApplicationUser User { get; set; }
    }

}
