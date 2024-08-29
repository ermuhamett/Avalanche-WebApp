using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Services
{
    public class AvalancheDataService
    {
        private readonly AvalancheDbContext _context;
        public AvalancheDataService(AvalancheDbContext context)
        {
            _context = context;
        }
        public async Task<RegionDetailsViewModel> GetRegionDetailsAsync(int regionId)
        {
            var region = await _context.Regions
                .Include(r => r.AvalancheData)
                .FirstOrDefaultAsync(m => m.RegionId == regionId);

            if (region == null)
            {
                return null;
            }

            var combinedData = region.AvalancheData
                .Select(ad => new CombinedData
                {
                    DataId = ad.DataId, // добавьте DataId здесь
                    Month = ad.Month,
                    Day = ad.Day,
                    AirTemperatureMorning = ad.AirTemperatureMorning,
                    SnowDepthAverageMorning = ad.SnowDepthAverageMorning,
                    SnowDepthMaxMorning = ad.SnowDepthMaxMorning,
                    WeatherMorning = ad.WeatherMorning,
                    AirTemperatureEvening = ad.AirTemperatureEvening,
                    SnowDepthAverageEvening = ad.SnowDepthAverageEvening,
                    SnowDepthMaxEvening = ad.SnowDepthMaxEvening,
                    WeatherEvening = ad.WeatherEvening,
                    AverageTemperatureDay = ad.AverageTemperatureDay,
                    AverageTemperatureDecade = ad.AverageTemperatureDecade,
                    Precipitation = ad.Precipitation,
                    AdditionalInfo = ad.AdditionalInfo
                }).ToList();

            var viewModel = new RegionDetailsViewModel
            {
                Region = region,
                CombinedData = combinedData
            };

            return viewModel;
        }
        public async Task<AvalancheData> GetAvalancheDataByIdAsync(int id)
        {
            return await _context.AvalancheData
                .FirstOrDefaultAsync(ad => ad.DataId == id);
        }     
        public async Task DeleteAvalancheDataAsync(int id)
        {
            var data = await _context.AvalancheData.FindAsync(id);
            if (data != null)
            {
                _context.AvalancheData.Remove(data);
                await _context.SaveChangesAsync();
            }
        }       
        // Метод для обновления данных о лавинах
        public async Task UpdateAvalancheDataAsync(List<UpdateCombinedData> updateCombinedData)
        {
            foreach (var data in updateCombinedData)
            {
                var avalancheData = await _context.AvalancheData.FindAsync(data.DataId);
                Console.WriteLine(avalancheData);
                if (avalancheData != null)
                {
                    avalancheData.Month = data.Month;
                    avalancheData.Day = data.Day;
                    avalancheData.AirTemperatureMorning = data.AirTemperatureMorning ?? avalancheData.AirTemperatureMorning;
                    avalancheData.SnowDepthAverageMorning = data.SnowDepthAverageMorning ?? avalancheData.SnowDepthAverageMorning;
                    avalancheData.SnowDepthMaxMorning = data.SnowDepthMaxMorning ?? avalancheData.SnowDepthMaxMorning;
                    avalancheData.WeatherMorning = data.WeatherMorning ?? avalancheData.WeatherMorning;
                    avalancheData.AirTemperatureEvening = data.AirTemperatureEvening ?? avalancheData.AirTemperatureEvening;
                    avalancheData.SnowDepthAverageEvening = data.SnowDepthAverageEvening ?? avalancheData.SnowDepthAverageEvening;
                    avalancheData.SnowDepthMaxEvening = data.SnowDepthMaxEvening ?? avalancheData.SnowDepthMaxEvening;
                    avalancheData.WeatherEvening = data.WeatherEvening ?? avalancheData.WeatherEvening;
                    avalancheData.AverageTemperatureDay = data.AverageTemperatureDay ?? avalancheData.AverageTemperatureDay;
                    avalancheData.AverageTemperatureDecade = data.AverageTemperatureDecade ?? avalancheData.AverageTemperatureDecade;
                    avalancheData.Precipitation = data.Precipitation;
                    avalancheData.AdditionalInfo = data.AdditionalInfo ?? avalancheData.AdditionalInfo;
                    _context.AvalancheData.Update(avalancheData);
                }
                else
                {
                    Console.WriteLine($"DataId not found: {data.DataId}");
                }
            }
            await _context.SaveChangesAsync();
        }
        public async Task CreateAvalancheDataAsync(AvalancheData avalancheData)
        {
            _context.AvalancheData.Add(avalancheData);
            await _context.SaveChangesAsync();
        }
    }
}
