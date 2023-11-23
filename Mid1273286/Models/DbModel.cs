using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mid1273286.Models
{
    public enum Size { S = 1, M, L, XL }
    public class Dress
    {
        public int DressId { get; set; }
        [Required, StringLength(50)]
        public string DressName { get; set; } = default!;
        [Required, EnumDataType(typeof(Size))]
        public Size? Size { get; set; } = default!;
        [Required, Column(TypeName = "money")]
        public decimal Price { get; set; }
        [Required, StringLength(50)]
        public string? Picture { get; set; } = default!;

        public bool? OnSale { get; set; }
        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    }
    public class Sale
    {
        public int SaleId { get; set; }
        [Required, Column(TypeName = "date")]
        public DateTime? Date { get; set; }
        public int? Quantity { get; set; }
        [Required, ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Dress? Dress { get; set; }
    }
    public class DressDbContext : DbContext
    {
        public DressDbContext(DbContextOptions<DressDbContext> options) : base(options) { }
        public DbSet<Dress> Dresses { get; set; } = default!;
        public DbSet<Sale> Sales { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            Random random = new Random();
            for (int i = 1; i <= 5; i++)
            {
                modelBuilder.Entity<Dress>().HasData(
                  new Dress { DressId = i, DressName = "Dress " + i, Size = (Size)random.Next(1, 5), Price = random.Next(1000, 2000) * 1.00M, OnSale = true, Picture = i + ".jpg" }

             );
            }
            for (int i = 1; i <= 8; i++)
            {
                modelBuilder.Entity<Sale>().HasData(
                   new Sale { SaleId = i, Date = DateTime.Now.AddDays(-1 * random.Next(200, 500)), Quantity = random.Next(100, 300), ProductId = i % 5 == 0 ? 5 : i % 5 }

               );

            }
        }
    }
}
