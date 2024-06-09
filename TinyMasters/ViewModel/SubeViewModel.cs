using TinyMasters.Models.Entity;

namespace TinyMasters.ViewModel
{
    public class SubeViewModel
    {
        public int SubeId { get; set; }
        public string SubeName { get; set; }
        public string reservedname { get; set; }
        public int statu { get; set; }
        public List<Product> Products { get; set; }
        public int ReservedId { get; set; }
    }
}
