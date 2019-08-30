using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Shared.Model
{
    /// <summary>
    ///  `(TotalRentalCost, Year, Make, Model, Owner)` 
    /// </summary>
    public class RentalResult
    {
        public RentalResult() { }

        [JsonConstructor]
        public RentalResult(int id, decimal totalRentalCost, int year, string make, string model, string owner)
        {
            Id = id;
            TotalRentalCost = totalRentalCost;
            Year = year;
            Make = make;
            Model = model;
            Owner = owner;
        }

        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("totalRentalCost")]
        public decimal TotalRentalCost { get; }

        [JsonProperty("year")]
        public int Year { get; }

        [JsonProperty("make")]
        public string Make { get; }

        [JsonProperty("model")]
        public string Model { get; }

        [JsonProperty("owner")]
        public string Owner { get; }
    }
}
