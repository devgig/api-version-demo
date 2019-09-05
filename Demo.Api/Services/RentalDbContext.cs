using Demo.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace Demo.Api.Services
{
    public class RentalDbContext : DbContext
    {
        public RentalDbContext(DbContextOptions<RentalDbContext> options) : base(options) {
        }
        public DbSet<Rental> RentalItems { get; set; }
    }
}
