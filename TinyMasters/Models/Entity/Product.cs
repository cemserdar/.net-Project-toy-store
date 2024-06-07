using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyMasters.Models.Entity
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Unit { get; set; }
        [ForeignKey("Sube")]
        public int SubeId { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
    }
}
