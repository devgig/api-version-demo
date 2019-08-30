using Demo.Shared.Model;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace Demo.Ui.Services
{
    public interface IRentalProvider
    {
        IEnumerable<Rental> GetByCriteria(string criteria);
    }
    public class RentalProvider : IRentalProvider
    {
        public IEnumerable<Rental> GetByCriteria(string criteria)
        {
            RestClient restClient = new RestClient("https://localhost:44308/api/v1/rental");
            RestRequest restRequest = new RestRequest(Method.GET);
            restRequest.AddParameter("criteria", criteria);
            var response = restClient.Execute(restRequest);

            return JsonConvert.DeserializeObject<IEnumerable<Rental>>(response.Content);
        }
    }
}
