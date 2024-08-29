namespace WebApp.Models
{
    public class CombinedData
    {
        public int DataId { get; set; }  // Идентификатор записи данных о лавинах, уникальный для каждой записи
        public int RegionId { get; set; }  // Ссылка на идентификатор региона
        public int Month { get; set; }
        public int Day { get; set; }
        public decimal AirTemperatureMorning { get; set; }
        public decimal SnowDepthAverageMorning { get; set; }
        public decimal SnowDepthMaxMorning { get; set; }
        public string WeatherMorning { get; set; }
        public decimal AirTemperatureEvening { get; set; }
        public decimal SnowDepthAverageEvening { get; set; }
        public decimal SnowDepthMaxEvening { get; set; }
        public string WeatherEvening { get; set; }
        public decimal Precipitation { get; set; }
        public string AdditionalInfo { get; set; }
        public decimal AverageTemperatureDay { get; set; }
        public decimal AverageTemperatureDecade { get; set; }
    }
    public class RegionDetailsViewModel
    {
        public Region Region { get; set; }
        public List<CombinedData> CombinedData { get; set; }
    }
}
