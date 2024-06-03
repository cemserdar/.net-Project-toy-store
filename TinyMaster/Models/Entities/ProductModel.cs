using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyMaster.Models.Entities
{
    public class ProductModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Isim { get; set; }
        public string FotoUrl { get; set; }
        public decimal Fiyat { get; set; }
        public string Aciklama { get; set; }
        public ICollection<OrderedItemModel> SiparisDetaylari { get; set; }
    }
}
