using System.ComponentModel;

namespace Demo.Api.Services
{
    public enum RentalColumn
    {
        [Description("id")]
        Id,
        [Description("make")]
        Make,
        [Description("model")]
        Model,
        [Description("year")]
        Year,
        [Description("vin")]
        Vin,
        [Description("country")]
        Country,
        [Description("owner")] //(Skip, garbage data)
        Owner,
        [Description("currentgaslevel")]
        CurrentGasLevel,
        [Description("dailyrate")]
        DailyRate


    }
}
