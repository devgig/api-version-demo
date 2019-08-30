using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Shared.Model;
using Demo.Shared.Extensions;

namespace Demo.Api.Services
{
    public interface IRentalService
    {
        Task<IEnumerable<RentalResult>> GetRentalsByCriteria(string criteria, int numberOfDays);
        Task<IEnumerable<RentalResult>> GetRental(string year, string make, string model, int numberOfDays);
        Task<bool> SaveRentals(IEnumerable<Rental> rentals);
    }
    public class RentalService : IRentalService
    {
        private readonly RentalDbContext _context;
        private readonly IRentalValidator _rentalValidator;

        public RentalService(RentalDbContext context, IRentalValidator rentalValidator)
        {
            _context = context;
            _rentalValidator = rentalValidator;
        }

        public async Task<IEnumerable<RentalResult>> GetRental(string year, string make, string model, int numberOfDays)
        {
            var results = _context.RentalItems.Where(x => x.Year == (year == null ? x.Year : year.ToNumber())
            && x.Make == (make == null ? x.Make : make)
            && (x.Model == (model == null ? x.Model : model) || x.Model.Contains(model)))
                .Select(x => new RentalResult(x.Id, x.TotalRentalCost(numberOfDays), x.Year, x.Make, x.Model, x.Owner))
                    .OrderBy(x => x.TotalRentalCost);

            return results;

        }

        public async Task<IEnumerable<RentalResult>> GetRentalsByCriteria(string criteria, int numberOfDays)
        {
            if (string.IsNullOrEmpty(criteria))
                return Enumerable.Empty<RentalResult>();
            else
            {
                var results = _context.RentalItems
                    .Where(x => x.Year == criteria.ToNumber() || x.Make == criteria || x.Model.Contains(criteria))
                    .Select(x => new RentalResult(x.Id, x.TotalRentalCost(numberOfDays), x.Year, x.Make, x.Model, x.Owner))
                    .OrderBy(x => x.TotalRentalCost);

                return results;
            }

        }

        public async Task<bool> SaveRentals(IEnumerable<Rental> rentals)
        {
            var validRentals = _rentalValidator.Validate(rentals);

            await _context.RentalItems.AddRangeAsync(validRentals);
            var result = await _context.SaveChangesAsync();

            return await Task.FromResult(result == validRentals.Count());
        }
    }
}
