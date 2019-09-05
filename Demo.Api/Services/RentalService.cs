using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Shared.Model;
using Demo.Shared.Extensions;

namespace Demo.Api.Services
{
    public interface IRentalService
    {
        Task<IEnumerable<RentalResult>> GetAllRentals();
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

            _context.RentalItems.Add(new Rental(1) { Make = "Toyota", Model = "Tundra", Year = 2004 });
            _context.RentalItems.Add(new Rental(2) { Make = "Toyota", Model = "Camry", Year = 2010 });
            _context.RentalItems.Add(new Rental(3) { Make = "Toyota", Model = "Tacoma", Year = 2008 });
            _context.RentalItems.Add(new Rental(4) { Make = "Toyota", Model = "Corolla", Year = 2006 });
            _context.RentalItems.Add(new Rental(5) { Make = "Toyota", Model = "Highlander", Year = 2014 });
            _context.RentalItems.Add(new Rental(6) { Make = "Ford", Model = "F-150", Year = 2004 });
            _context.RentalItems.Add(new Rental(7) { Make = "Ford", Model = "Explorer", Year = 2010 });
            _context.RentalItems.Add(new Rental(8) { Make = "Ford", Model = "Expidition", Year = 2008 });
            _context.RentalItems.Add(new Rental(9) { Make = "Ford", Model = "Mustang", Year = 2006 });
            _context.RentalItems.Add(new Rental(10) { Make = "Ford", Model = "Focus", Year = 2014 });

            _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RentalResult>> GetAllRentals()
        {
            
            return _context.RentalItems.Where(x => x.Make == "Toyota").Take(1).Concat(_context.RentalItems.Where(x => x.Make == "Ford").Take(1))
                .Select(x => new RentalResult(x.Id, x.TotalRentalCost(0), x.Year, x.Make, x.Model, x.Owner)).ToArray();


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
                    .Where(x => x.Year == criteria.ToNumber() || x.Make == criteria || x.Model.ToUpper().Contains(criteria.ToUpper()))
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
