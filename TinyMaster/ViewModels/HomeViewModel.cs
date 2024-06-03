using TinyMaster.Models.Entities;

namespace TinyMaster.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
        }

        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public decimal UrunFiyat { get; set; }
        public string FotoUrl { get; set; }
        public string Aciklama { get; set; }
        public OrderedItemModel OrderedItem { get; set; }
        public ProductModel ProductModel { get; set; }
        public OrderModel OrderModel { get; set; }
    }
}
