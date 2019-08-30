using Demo.Api.Services;
using RestSharp;
using System.Net;
using Demo.Shared.Extensions;
using Demo.Ui.Core;
using Demo.Ui.Extensions;
using Demo.Shared.Model;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Demo.Ui.Services
{
    public interface IRentalUploader
    {
        Task<bool> Upload(string uploadFile);
    }

    public class RentalUploader
    {
        public async Task<bool> Upload(string uploadFile)
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

            var json = JsonConvert.SerializeObject(list.ToArray());
            RestClient restClient = new RestClient("https://localhost:44308/api/v1/rental");
            RestRequest restRequest = new RestRequest(Method.POST);
            restRequest.AddParameter("application/json", json, ParameterType.RequestBody);
            var response = await restClient.ExecutePostTaskAsync(restRequest);
            return response.StatusCode == HttpStatusCode.OK;
        }


    }
}
