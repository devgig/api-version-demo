using Microsoft.EntityFrameworkCore;

namespace Demo.Api.V1.Models
{
    public class RentalDbContext : DbContext
    {
        public RentalDbContext(DbContextOptions<RentalDbContext> options) : base(options) { }
        public DbSet<Rental> RentalItems { get; set; }
    }
}
