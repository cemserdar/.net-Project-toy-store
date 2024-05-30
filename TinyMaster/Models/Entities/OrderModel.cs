using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TinyMaster.Models.Entities
{
    public class OrderModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime SiparisTarihi { get; set; }
        public decimal ToplamFiyat { get; set; }
        public int MusteriId { get; set; }
        public CustomerModel Musteri { get; set; }
        public ICollection<OrderedItemModel> SiparisDetaylari { get; set; }
    }
}
