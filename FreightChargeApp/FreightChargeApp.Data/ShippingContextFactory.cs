using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FreightChargeApp.Data
{
    public class ShippingContextFactory : IDesignTimeDbContextFactory<ShippingContext>
    {
        public ShippingContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<ShippingContext> optionsBuilder = new();
            optionsBuilder.UseSqlServer("Server=localhost\\SQLExpress;Database=FreightChargeDb;Trusted_Connection=True");

            return new ShippingContext(optionsBuilder.Options);
        }
    }
}
