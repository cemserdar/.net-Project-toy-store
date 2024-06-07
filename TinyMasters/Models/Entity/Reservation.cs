using System.ComponentModel.DataAnnotations.Schema;

namespace TinyMasters.Models.Entity
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime Tarih { get; set; }
        [ForeignKey("User")]
        public int User { get; set; }
        public string HangiSube { get; set; }
        [ForeignKey("Product")]
        public int Product { get; set; }
    }
}
