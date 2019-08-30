using System;
using System.IO;
using System.Threading.Tasks;
using Demo.Api.Core;
using Demo.Api.V1.Models;

namespace Demo.Api.Services
{
    public interface IRentalImportService
    {
        Task<bool> Import(Stream stream);
    }

    
    public class RentalImportService : IRentalImportService
    {
        private readonly RentalDbContext _context;
        public RentalImportService(RentalDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Import(Stream stream)
        {
            var id = RentalColumn.Id.Description();
            var make = RentalColumn.Make.Description();
            var model = RentalColumn.Model.Description();
            var year = RentalColumn.Year.Description();
            var dailyrate = RentalColumn.DailyRate.Description();

            Stream local = new MemoryStream();
            await stream.CopyToAsync(local);
          

            var csv = new CsvFile(local);
            foreach(var line in csv.LazyRead())
            {
                var rental = new Rental(line[id].ToNumber())
                {
                    Make = line[make],
                    Model = line[model],
                    Owner = null, //skipped due to garbage data
                    Year = line[year].ToNumber(),
                    DailyRate = line[dailyrate].ToDecimal()
                };
                rental.SupportsOb2 = rental.Year > 1996;
                await _context.RentalItems.AddAsync(rental);
            }
            
            var result = await _context.SaveChangesAsync();
            
            return await Task.FromResult(result > 0);
        }


    }
}
