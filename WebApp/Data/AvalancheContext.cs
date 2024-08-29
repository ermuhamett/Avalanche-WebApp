using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data
{
    public class AvalancheDbContext:DbContext
    {
        public DbSet<Region> Regions { get; set; }
        public DbSet<AvalancheData> AvalancheData { get; set; }
        public AvalancheDbContext(DbContextOptions<AvalancheDbContext> options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Region>()
                .HasKey(r => r.RegionId);

            modelBuilder.Entity<AvalancheData>()
                .HasKey(ad => ad.DataId);

            modelBuilder.Entity<AvalancheData>()
                .HasOne(ad => ad.Region)
                .WithMany(r => r.AvalancheData)
                .HasForeignKey(ad => ad.RegionId)
                .OnDelete(DeleteBehavior.Cascade); // Enable cascade delete
        }
    }
}
