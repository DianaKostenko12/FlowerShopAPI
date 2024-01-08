using FlowerShop.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Flower> Flowers { get; set; }
        public DbSet<Bouquet> Bouquets { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<BouquetFlower> BouquetFlowers { get; set; }
        public DbSet<OrderBouquet> OrderBouquets { get; set; }
        public DbSet<OrderGift> OrderGifts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BouquetFlower>()
                .HasKey(bf => new { bf.BouquetId, bf.FlowerId });
            modelBuilder.Entity<BouquetFlower>()
                .HasOne(b => b.Bouquet)
                .WithMany(bf => bf.BouquetsFlowers)
                .HasForeignKey(b => b.BouquetId);
            modelBuilder.Entity<BouquetFlower>()
                .HasOne(f => f.Flower)
                .WithMany(bf => bf.BouquetsFlowers)
                .HasForeignKey(f => f.FlowerId);

            modelBuilder.Entity<OrderBouquet>()
                .HasKey(ob => new { ob.OrderId, ob.BouquetId });
            modelBuilder.Entity<OrderBouquet>()
                .HasOne(b => b.Bouquet)
                .WithMany(ob => ob.OrderBouquets)
                .HasForeignKey(b => b.BouquetId);
            modelBuilder.Entity<OrderBouquet>()
                .HasOne(o => o.Order)
                .WithMany(ob => ob.OrderBouquets)
                .HasForeignKey(o => o.OrderId);

            modelBuilder.Entity<OrderGift>()
                .HasKey(og => new { og.OrderId, og.GiftId });
            modelBuilder.Entity<OrderGift>()
                .HasOne(g => g.Gift)
                .WithMany(og => og.OrderGifts)
                .HasForeignKey(g => g.GiftId);
            modelBuilder.Entity<OrderGift>()
                .HasOne(o => o.Order)
                .WithMany(og => og.OrderGifts)
                .HasForeignKey(o => o.OrderId);
        }
    }
}
