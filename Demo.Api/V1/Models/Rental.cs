using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.Api.V1.Models
{
    public class Rental
    {

        public Rental() { }
        public Rental(int id)
        {
            Id = id;
        }

        [Key]
        public int Id { get; set; }
        public decimal DailyRate { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Owner { get; set; }
        public bool SupportsOb2 { get; internal set; }
    }
}
