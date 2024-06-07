using System.ComponentModel.DataAnnotations.Schema;
using TinyMasters.Models.Entity;

namespace TinyMasters.ViewModel
{
    public class ReservationViewModel
    {
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int SubeId { get; set; }
        public List<string> SubeAdi { get; set; }
        public int ProductId { get; set; }
        public int KisiSayisi { get; set; }
        public bool Onay { get; set; }

    }
}
