namespace TinyMaster.Models.Entitiy
{
    public class CartModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Unit { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
