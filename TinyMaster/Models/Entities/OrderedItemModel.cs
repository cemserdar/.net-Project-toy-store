using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TinyMaster.Models.Entities
{
    public class OrderedItemModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SiparisId { get; set; }
        public OrderModel Siparis { get; set; }
        public int UrunId { get; set; }
        public ProductModel Urun { get; set; }
        public int Miktar { get; set; }
        public decimal Fiyat { get; set; }
    }
}
