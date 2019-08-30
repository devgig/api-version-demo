using Demo.Shared.Model;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Api.Services
{
    public interface IRentalValidator
    {
        IEnumerable<Rental> Validate(IEnumerable<Rental> rentals);
    }
    public class RentalValidator : IRentalValidator
    {
        public IEnumerable<Rental> Validate(IEnumerable<Rental> rentals)
        {
            return rentals.Where(x => x.Year > 1996).ToArray();
        }
    }
}
