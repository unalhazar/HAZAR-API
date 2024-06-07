using System.ComponentModel.DataAnnotations;

namespace Domain.Base
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }

        public short Durum { get; set; }

        public DateTime? OlusturulmaTarih { get; set; }

        public long? OlusturanKullaniciId { get; set; }

        public DateTime? GuncellenmeTarih { get; set; }

        public long? GuncelleyenKullaniciId { get; set; }

        public DateTime? SilinmeTarih { get; set; }

        public long? SilenKullaniciId { get; set; }

        public Guid? Anahtar { get; set; }
    }
}
