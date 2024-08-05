using System.ComponentModel.DataAnnotations;

namespace Domain.Base
{
    public abstract class BaseEntity
    {
        [Key]
        public long Id { get; set; }

        public int? State { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? CreatedUserId { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? UpdatedUserId { get; set; }

        public DateTime? DeletedDate { get; set; }

        public long? DeletedUserId { get; set; }

        public Guid? Key { get; set; }
    }
}
