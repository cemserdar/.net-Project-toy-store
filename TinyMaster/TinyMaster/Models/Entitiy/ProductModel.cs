namespace TinyMaster.Models.Entitiy
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string Description { get; set; }
        public int UnitInStock { get; set; }
    }
}
