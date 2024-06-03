using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
