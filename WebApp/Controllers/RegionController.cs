using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using WebApp.Data;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class RegionController : Controller
    {
        private readonly ExcelDataImporter _excelDataImporter;
        private readonly RegionDataService _regionDataService;
        private readonly AvalancheDataService _avalancheDataService;

        public RegionController(RegionDataService regionDataService, ExcelDataImporter excelDataImporter, AvalancheDataService avalancheDataService)
        {
            _regionDataService = regionDataService;
            _excelDataImporter = excelDataImporter;
            _avalancheDataService = avalancheDataService;
        }

        // Список регионов
        public async Task<IActionResult> Index()
        {
            var regions = await _regionDataService.GetRegionsAsync();
            return View(regions);
        }

        // Метод для отображения страницы загрузки файла
        public IActionResult Upload()
        {
            return View();
        }
        // Отображение страницы создания региона
        public IActionResult Create()
        {
            return View();
        }

        // Метод для создания региона
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Region region)
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }
            if (ModelState.IsValid)
            {
                Console.WriteLine($"Region Name: {region.RegionName}");
                await _regionDataService.CreateRegionAsync(region);
                return RedirectToAction(nameof(Index));
            }
            return View(region);
        }

        // Отображение страницы редактирования региона
        public async Task<IActionResult> Edit(int id)
        {
            var region = await _regionDataService.GetRegionByIdAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            return View(region);
        }

        // Метод для редактирования региона
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegionId,RegionName")] Region region)
        {
            if (id != region.RegionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _regionDataService.UpdateRegionAsync(region);
                return RedirectToAction(nameof(Index));
            }
            return View(region);
        }

        // Метод для удаления региона
        public async Task<IActionResult> Delete(int id)
        {
            var region = await _regionDataService.GetRegionByIdAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            return View(region);
        }
        // Метод для подтверждения удаления региона
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _regionDataService.DeleteRegionAsync(id);
            return RedirectToAction(nameof(Index));
        }
        // Метод для обработки POST-запроса на загрузку файла
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                try
                {
                    var filePath = Path.GetTempFileName();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    await _excelDataImporter.ImportDataFromExcelAsync(filePath);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Логирование ошибки
                    Console.WriteLine($"Ошибка при обработке файла: {ex.Message}");
                    ModelState.AddModelError("", "Произошла ошибка при обработке файла.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Файл не выбран или пустой.");
            }
            return View();
        }

        // Метод для удаления всех данных
        [HttpPost]
        public async Task<IActionResult> DeleteAll()
        {
            await _regionDataService.DeleteAllDataAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id, int selectedMonth = 0)
        {
            var regionDetails = await _avalancheDataService.GetRegionDetailsAsync(id);

            if (regionDetails == null)
            {
                return NotFound();
            }

            // Filter data if a specific month is selected
            if (selectedMonth > 0)
            {
                regionDetails.CombinedData = regionDetails.CombinedData
                    .Where(cd => cd.Month == selectedMonth)
                    .OrderBy(cd => cd.Month)
                    .ThenBy(cd => cd.Day)
                    .ToList();
            }
            else
            {
                // Sort the data by month and day if no filter is applied
                regionDetails.CombinedData = regionDetails.CombinedData
                    .OrderBy(cd => cd.Month)
                    .ThenBy(cd => cd.Day)
                    .ToList();
            }
            var model = new RegionDetailsWithFilter
            {
                Region = regionDetails.Region,
                CombinedData = regionDetails.CombinedData,
                SelectedMonth = selectedMonth,
                Months = Enumerable.Range(1, 12)
                .Select(x => new SelectListItem { Value = x.ToString(), Text = new DateTime(1, x, 1).ToString("MMMM") })
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult FilterByMonth(int regionId, int selectedMonth)
        {
            return RedirectToAction(nameof(Details), new { id = regionId, selectedMonth });
        }
    }
}
      
