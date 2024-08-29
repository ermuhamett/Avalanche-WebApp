using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using WebApp.Data;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class AvalancheDataController : Controller
    {
        private readonly AvalancheDataService _avalancheDataService;

        public AvalancheDataController(AvalancheDataService avalancheDataService)
        {
            _avalancheDataService = avalancheDataService;
        }

        // Отображение всех данных о лавинах для региона
        public async Task<IActionResult> Index(int regionId)
        {
            var viewModel = await _avalancheDataService.GetRegionDetailsAsync(regionId);
            if (viewModel == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        // Создание данных о лавинах
        public IActionResult Create(int regionId)
        {
            var model = new AvalancheData { RegionId = regionId };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AvalancheData avalancheData)
        {
            if (ModelState.IsValid)
            {
                await _avalancheDataService.CreateAvalancheDataAsync(avalancheData);
                return RedirectToAction(nameof(Index), new { regionId = avalancheData.RegionId });
            }
            return View(avalancheData);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var avalancheData = await _avalancheDataService.GetAvalancheDataByIdAsync(id);
            if (avalancheData == null)
            {
                return NotFound();
            }
            return View(avalancheData);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _avalancheDataService.DeleteAvalancheDataAsync(id);
            return RedirectToAction("Index", "Region");
        }

        // GET: AvalancheData/MonthCard Получить данные по месяцам в виде карточек
        public async Task<IActionResult> MonthCard(int regionId)
        {
            var regionDetails = await _avalancheDataService.GetRegionDetailsAsync(regionId);

            if (regionDetails == null)
            {
                return NotFound();
            }

            var viewModel = new RegionDetailsViewModel
            {
                Region = regionDetails.Region,
                CombinedData = regionDetails.CombinedData
            };

            return View(viewModel);
        }

        // GET: AvalancheData/EditDetails/5 Редактирования деталей
        public async Task<IActionResult> EditDetails(int regionId, int month)
        {
            var regionDetails = await _avalancheDataService.GetRegionDetailsAsync(regionId);

            if (regionDetails == null)
            {
                return NotFound();
            }
            var updateDetailsViewModel = new UpdateDetailsViewModel
            {
                Region = regionDetails.Region,
                UpdateCombinedData = regionDetails.CombinedData
                .Where(cd => cd.Month == month)
                .Select(cd => new UpdateCombinedData
            {
                DataId = cd.DataId,
                RegionId = cd.RegionId,
                Month = cd.Month,
                Day = cd.Day,
                AirTemperatureMorning = cd.AirTemperatureMorning,
                SnowDepthAverageMorning = cd.SnowDepthAverageMorning,
                SnowDepthMaxMorning = cd.SnowDepthMaxMorning,
                WeatherMorning = cd.WeatherMorning,
                AirTemperatureEvening = cd.AirTemperatureEvening,
                SnowDepthAverageEvening = cd.SnowDepthAverageEvening,
                SnowDepthMaxEvening = cd.SnowDepthMaxEvening,
                WeatherEvening = cd.WeatherEvening,
                AverageTemperatureDay = cd.AverageTemperatureDay,
                AverageTemperatureDecade = cd.AverageTemperatureDecade,
                Precipitation = cd.Precipitation,
                AdditionalInfo = cd.AdditionalInfo,     
            }).ToList()
            };

            return View(updateDetailsViewModel);
        }

        // POST: AvalancheData/EditDetails Сохранения изменения
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDetails(UpdateDetailsViewModel viewModel)
        { 
            if(ModelState.IsValid)
            {
                Console.WriteLine(viewModel.UpdateCombinedData);
                await _avalancheDataService.UpdateAvalancheDataAsync(viewModel.UpdateCombinedData);
                return RedirectToAction("Details", "Region", new { id = viewModel.Region.RegionId });   
            }
            // Логирование ошибок валидации
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            return View(viewModel);
        }
    }
}

//asp - controller = "AvalancheData" asp - action = "EditDetails"
