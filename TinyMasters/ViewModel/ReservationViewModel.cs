using System.ComponentModel.DataAnnotations;

namespace TinyMasters.ViewModel
{
    public class ReservationViewModel
    {
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
  
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public List<SubeViewModel> SubeList { get; set; }
        public int SubeId { get; set; }
        public int ProductId { get; set; }
        public int KisiSayisi { get; set; }
        public bool Onay { get; set; }

    }
}
