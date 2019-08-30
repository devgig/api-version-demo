using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Shared.Model;
using Demo.Shared.Extensions;

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
            return _context.RentalItems.Where(x => x.Year == (year == null ? x.Year : year.ToNumber())
            && x.Make == (make == null ? x.Make : make) 
            && x.Model == (model == null ? x.Model : model));
        }

        public async Task<IEnumerable<Rental>> GetRentalsByCriteria(string criteria)
        {
            if (string.IsNullOrEmpty(criteria))
                return Enumerable.Empty<Rental>();
            else
                return _context.RentalItems.Where(x => x.Year == criteria.ToNumber() || x.Make == criteria || x.Model == criteria);

        }

        public async Task<bool> SaveRentals(IEnumerable<Rental> rentals)
        {
            await _context.RentalItems.AddRangeAsync(rentals.ToArray());
            var result = await _context.SaveChangesAsync();

            return await Task.FromResult(result == rentals.Count());
        }
    }
}
