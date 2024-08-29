namespace WebApp.Models
{
    public class AvalancheData
    {
        public int DataId { get; set; }  // Идентификатор записи данных о лавинах, уникальный для каждой записи
        public int RegionId { get; set; }  // Ссылка на идентификатор региона
        public Region Region { get; set; }  // Ссылка на объект региона
        public int Month { get; set; }  // Месяц (из Excel)
        public int Day { get; set; }  // Число (из Excel)
        public decimal AirTemperatureMorning { get; set; }  // Температура воздуха в 7:00, град (из Excel)
        public decimal SnowDepthAverageMorning { get; set; }  // Снег в 7:00, см (ср) (из Excel)
        public decimal SnowDepthMaxMorning { get; set; }  // Снег в 7:00, см (макс) (из Excel)
        public string WeatherMorning { get; set; }  // Погода в 7:00 (из Excel)
        public decimal AirTemperatureEvening { get; set; }  // Температура воздуха в 19:00, град (из Excel)
        public decimal SnowDepthAverageEvening { get; set; }  // Снег в 19:00, см (ср) (из Excel)
        public decimal SnowDepthMaxEvening { get; set; }  // Снег в 19:00, см (макс) (из Excel)
        public string WeatherEvening { get; set; }  // Погода в 19:00 (из Excel)
        public decimal AverageTemperatureDay { get; set; }  // Температура воздуха средняя, град (сут) (из DailyAverage)
        public decimal AverageTemperatureDecade { get; set; }  // Температура воздуха средняя, град (декада) (из DailyAverage)
        public decimal Precipitation { get; set; }  // Количество осадков, мм (из Excel)
        public string AdditionalInfo { get; set; }  // Прочее (из Excel)
    }
}
