using Microsoft.EntityFrameworkCore;
using TinyMaster.Models.Entities;

namespace TinyMaster.Models
{

    public class TinyMasterDbContext : DbContext
    {
        public TinyMasterDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CustomerModel> Musteriler { get; set; }
        public DbSet<RezervationModel> UrunAyirmalar { get; set; }
        public DbSet<OrderModel> Siparisler { get; set; }
        public DbSet<OrderedItemModel> SiparisDetaylari { get; set; }
        public DbSet<ProductModel> Urunler { get; set; }
        public DbSet<BranchModel> Subeler { get; set; }




        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder

        //        .UseSqlServer(@"Server=localhost;Database=tinymaster;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true;");
        //}
    }
}

