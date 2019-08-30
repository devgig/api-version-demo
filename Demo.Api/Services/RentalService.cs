using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Shared.Model;

namespace Demo.Api.Services
{
    public interface IRentalService
    {
        Task<IEnumerable<Rental>> GetRentalsByCriteria(string criteria);
        Task<IEnumerable<Rental>> GetRental(string year, string make, string model);
        Task<bool> SaveRentals(IEnumerable<Rental> rentals);
    }
    public class RentalService : IRentalService
    {
        private readonly RentalDbContext _context;
        public RentalService(RentalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rental>> GetRental(string year, string make, string model)
        {
            return await Task.FromResult(_context.RentalItems.ToList());
        }

        public async Task<IEnumerable<Rental>> GetRentalsByCriteria(string criteria)
        {
            return await Task.FromResult(_context.RentalItems.ToList());
        }

        public async Task<bool> SaveRentals(IEnumerable<Rental> rentals)
        {
            await _context.RentalItems.AddRangeAsync(rentals.ToArray());
            var result = await _context.SaveChangesAsync();

            return await Task.FromResult(result == rentals.Count());
        }
    }
}
