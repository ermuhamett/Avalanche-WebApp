using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Services
{
    public class ExcelDataImporter
    {
        private readonly AvalancheDbContext _context;

        public ExcelDataImporter(AvalancheDbContext context)
        {
            _context = context;
        }
        public async Task ImportDataFromExcelAsync(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                foreach (var sheet in package.Workbook.Worksheets)
                {
                    var regionData = await _context.Regions
                        .FirstOrDefaultAsync(r => r.RegionName == sheet.Name);

                    if (regionData == null)
                    {
                        regionData = new Region { RegionName = sheet.Name };
                        _context.Regions.Add(regionData);
                        await _context.SaveChangesAsync();
                    }

                    var rowCount = sheet.Dimension.Rows;

                    for (int row = 4; row <= rowCount; row++)
                    {
                        try
                        {
                            var month = ParseInt(sheet.Cells[row, 1].Text); // Column A
                            var day = ParseInt(sheet.Cells[row, 2].Text); // Column B
                            var airTemperatureMorning = ParseDecimal(sheet.Cells[row, 3].Text); // Column C
                            var snowDepthAverageMorning = ParseDecimal(sheet.Cells[row, 4].Text); // Column D
                            var snowDepthMaxMorning = ParseDecimal(sheet.Cells[row, 5].Text); // Column E
                            var weatherMorning = sheet.Cells[row, 6].Text; // Column F
                            var airTemperatureEvening = ParseDecimal(sheet.Cells[row, 7].Text); // Column G
                            var snowDepthAverageEvening = ParseDecimal(sheet.Cells[row, 8].Text); // Column H
                            var snowDepthMaxEvening = ParseDecimal(sheet.Cells[row, 9].Text); // Column I
                            var weatherEvening = sheet.Cells[row, 10].Text; // Column J
                            var averageTemperatureDay = ParseDecimal(sheet.Cells[row, 11].Text); // Column K
                            var averageTemperatureDecade = ParseDecimal(sheet.Cells[row, 12].Text); // Column L
                            var precipitation = ParseDecimal(sheet.Cells[row, 13].Text); // Column M
                            var additionalInfo = sheet.Cells[row, 14].Text; // Column N
                           
                            var avalancheData = new AvalancheData
                            {
                                RegionId = regionData.RegionId,
                                Month = month,
                                Day = day,
                                AirTemperatureMorning = airTemperatureMorning,
                                SnowDepthAverageMorning = snowDepthAverageMorning,
                                SnowDepthMaxMorning = snowDepthMaxMorning,
                                WeatherMorning = weatherMorning,
                                AirTemperatureEvening = airTemperatureEvening,
                                SnowDepthAverageEvening = snowDepthAverageEvening,
                                SnowDepthMaxEvening = snowDepthMaxEvening,
                                WeatherEvening = weatherEvening,
                                AverageTemperatureDay = averageTemperatureDay,
                                AverageTemperatureDecade = averageTemperatureDecade,
                                Precipitation = precipitation,
                                AdditionalInfo = additionalInfo
                            };

                            _context.AvalancheData.Add(avalancheData);
                        }
                        catch (FormatException ex)
                        {
                            // Логирование или обработка ошибки
                            Console.WriteLine($"Ошибка при парсинге строки {row}: {ex.Message}");
                        }
                    }
                }
            }

            await _context.SaveChangesAsync();
        }

        private int ParseInt(string text)
        {
            return int.TryParse(text, out var result) ? result : 0;
        }

        private decimal ParseDecimal(string text)
        {
            return decimal.TryParse(text, out var result) ? result : 0;
        }
    }
}
