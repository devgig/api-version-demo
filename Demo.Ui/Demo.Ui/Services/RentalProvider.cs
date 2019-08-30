using Demo.Shared.Model;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Ui.Services
{
    public interface IRentalProvider
    {
        Task<IEnumerable<RentalResult>> GetByCriteria(string criteria, int numberOfDays);
    }
    public class RentalProvider : IRentalProvider
    {
        public async  Task<IEnumerable<RentalResult>> GetByCriteria(string criteria, int numberOfDays)
        {
            RestClient restClient = new RestClient("https://localhost:44308/api/v1/rental");
            RestRequest restRequest = new RestRequest(Method.GET);
            restRequest.AddParameter("criteria", criteria);
            restRequest.AddParameter("numberOfDays", numberOfDays);
            var response = restClient.Execute(restRequest);

            var rentals = JsonConvert.DeserializeObject<IEnumerable<RentalResult>>(response.Content);
            return await Task.FromResult(rentals);
        }
    }
}
