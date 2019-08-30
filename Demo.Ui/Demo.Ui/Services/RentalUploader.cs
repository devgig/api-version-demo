using Demo.Api.Services;
using RestSharp;
using System.Net;
using Demo.Shared.Extensions;
using Demo.Ui.Core;
using Demo.Ui.Extensions;
using Demo.Shared.Model;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Ui.Services
{
    public class RentalUploader
    {
        public bool Upload(string uploadFile)
        {

            var id = RentalColumn.Id.Description();
            var make = RentalColumn.Make.Description();
            var model = RentalColumn.Model.Description();
            var year = RentalColumn.Year.Description();
            var dailyrate = RentalColumn.DailyRate.Description();
            var list = new LinkedList<Rental>();
            
            var csv = new CsvFile(uploadFile);
            foreach (var line in csv.LazyRead())
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
                list.AddLast(rental);
            }

            return true;
            //RestClient restClient = new RestClient("https://localhost:44308/api/v1/upload");
            //RestRequest restRequest = new RestRequest(Method.POST);
            //restRequest.AddHeader("Content-Type", "multipart/form-data");
            //restRequest.AddFile("file", uploadFile);
            //var response = restClient.Execute(restRequest);
            //return response.StatusCode == HttpStatusCode.OK;
        }


    }
}
