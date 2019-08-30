using Demo.Shared.Model;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Ui.Services
{
    public interface IRentalV1Provider
    {
        Task<IEnumerable<RentalResult>> GetByCriteria(string criteria, int numberOfDays);
    }
    public class RentalV1Provider : IRentalV1Provider
    {
        public async  Task<IEnumerable<RentalResult>> GetByCriteria(string criteria, int numberOfDays)
        {
            RestClient restClient = new RestClient("https://localhost:44308/api/v1/rental");
            RestRequest restRequest = new RestRequest(Method.GET);
            restRequest.AddParameter("criteria", criteria);
            restRequest.AddParameter("numberOfDays", numberOfDays);
            var response = await restClient.ExecuteGetTaskAsync(restRequest);

            var rentals = JsonConvert.DeserializeObject<IEnumerable<RentalResult>>(response.Content);
            return rentals;
        }
    }

    public interface IRentalV2Provider
    {
        Task<IEnumerable<RentalResult>> GetByCriteria(string year, string make, string model, int numberOfDays);
    }
    public class RentalV2Provider : IRentalV2Provider
    {
     
        public async Task<IEnumerable<RentalResult>> GetByCriteria(string year, string make, string model, int numberOfDays)
        {
            RestClient restClient = new RestClient("https://localhost:44308/api/v2/rental");
            RestRequest restRequest = new RestRequest(Method.GET);
            restRequest.AddParameter("year", year);
            restRequest.AddParameter("make", make);
            restRequest.AddParameter("model", model);
            restRequest.AddParameter("numberOfDays", numberOfDays);
            var response = await restClient.ExecuteGetTaskAsync(restRequest);

            var rentals = JsonConvert.DeserializeObject<IEnumerable<RentalResult>>(response.Content);
            return rentals;
        }
    }
}
