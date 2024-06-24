using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class TokenBlacklist
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }
    }
}
