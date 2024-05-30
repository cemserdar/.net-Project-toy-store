using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TinyMaster.Models.Entities
{
    public class BranchModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Isim { get; set; }
        public string Adres { get; set; }
        public ICollection<RezervationModel> Rezervation { get; set; }
    }
}
