using Microsoft.EntityFrameworkCore;

namespace FreightChargeApp.Data
{
    public class ShippingContext : DbContext
    {
        public DbSet<Courier> Couriers => Set<Courier>();
        public DbSet<ShippingData> ShippingData => Set<ShippingData>();

        public ShippingContext(DbContextOptions<ShippingContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.Entity<Courier>().HasData(new[]
            {
                new Courier("Cargo4You") { Id = 1 },
                new Courier("ShipFaster") { Id = 2 },
                new Courier("MaltaShip") { Id = 3 },
            });
    }
}
