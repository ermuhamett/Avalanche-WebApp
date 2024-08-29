using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class UpdateCombinedData
    {
        public int DataId { get; set; }
        public int RegionId { get; set; }
        [Range(1, 12, ErrorMessage = "Month must be between 1 and 12")]
        public int Month { get; set; }
        [Range(1, 31, ErrorMessage = "Day must be between 1 and 31")]
        public int Day { get; set; }
        public decimal? AirTemperatureMorning { get; set; } = 0m;
        public decimal? SnowDepthAverageMorning { get; set; } = 0m;
        public decimal? SnowDepthMaxMorning { get; set; } = 0m;
        //[Required(ErrorMessage = "WeatherMorning is required")]
        public string? WeatherMorning { get; set; } = string.Empty;
        public decimal? AirTemperatureEvening { get; set; } = 0m;
        public decimal? SnowDepthAverageEvening { get; set; } = 0m;
        public decimal? SnowDepthMaxEvening { get; set; } = 0m;
        //[Required(ErrorMessage = "WeatherEvening is required")]
        public string? WeatherEvening { get; set; } = string.Empty;
        public decimal Precipitation { get; set; } = 0m;
        //[Required(ErrorMessage = "AdditionalInfo is required")]
        public string? AdditionalInfo { get; set; }= string.Empty;
        public decimal? AverageTemperatureDay { get; set; } = 0m;
        public decimal? AverageTemperatureDecade { get; set; } = 0m;
    }
    public class UpdateDetailsViewModel
    {
        //[BindNever]
        public Region? Region { get; set; }
        public List<UpdateCombinedData> UpdateCombinedData { get; set; }
    }
}
