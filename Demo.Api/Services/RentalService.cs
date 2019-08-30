using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Api.V1.Models;

namespace Demo.Api.Services
{
    public interface IRentalService
    {
        Task<IEnumerable<Rental>> GetRentalsByCriteria(string criteria);
        Task<IEnumerable<Rental>> GetRental(string year, string make, string model);
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
    }
}
