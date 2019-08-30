using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Demo.Shared.Model
{
    
    public class Rental
    {
        [JsonConstructor]
        public Rental() { }

        public Rental(int id)
        {
            Id = id;
        }

        [JsonProperty("id")]
        [Key]
        public int Id { get; set; }

        [JsonProperty("dailyrate")]
        public decimal DailyRate { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("make")]
        public string Make { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("supportedOb2")]
        public bool SupportsOb2 { get; set; }
    }
}
