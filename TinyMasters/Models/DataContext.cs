using Microsoft.EntityFrameworkCore;
using TinyMasters.Models.Entity;

namespace TinyMasters.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> ProductTbl { get; set; }
        public DbSet<Order> OrderTlb { get; set; }
        public DbSet<Category> CategorieTbl { get; set; }
        public DbSet<Reservation> ReservationTbl { get; set; }
        public DbSet<Sube> SubeTbl { get; set; }
        public DbSet<User> UserTbl { get; set; }

    }
}
