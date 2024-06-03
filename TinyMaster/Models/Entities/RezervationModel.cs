using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyMaster.Models.Entities
{
    public class RezervationModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Tarih { get; set; }
        public TimeSpan Saat { get; set; }
        public int KisiSayisi { get; set; }
        public int SubeId { get; set; }
        public BranchModel Sube { get; set; }
        public bool Onaylandi { get; set; }
        public int MusteriId { get; set; }
        public CustomerModel Musteri { get; set; }
    }
}
