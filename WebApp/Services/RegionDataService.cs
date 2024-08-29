using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;


namespace WebApp.Services
{
    public class RegionDataService
    {
        private readonly AvalancheDbContext _context;

        public RegionDataService(AvalancheDbContext context)
        {
            _context = context;

        }
        //Создание региона
        public async Task CreateRegionAsync(Region region)
        {
            _context.Regions.Add(region);
            await _context.SaveChangesAsync();
        }
        //Список регионов
        public async Task<List<Region>> GetRegionsAsync()
        {
            return await _context.Regions.ToListAsync();
        }
        //Получаем нужный регион для изменения
        public async Task<Region> GetRegionByIdAsync(int id)
        {
            return await _context.Regions.FindAsync(id);
        }
        //Обновляем нужный регион
        public async Task UpdateRegionAsync(Region region)
        {
            _context.Regions.Update(region);
            await _context.SaveChangesAsync();
        }
        //Удаление региона
        public async Task DeleteRegionAsync(int id)
        {
            var region = await _context.Regions.FindAsync(id);
            if (region != null)
            {
                _context.Regions.Remove(region);
                await _context.SaveChangesAsync();
            }
        }
        // Удаление всех данных
        public async Task DeleteAllDataAsync()
        {
            _context.AvalancheData.RemoveRange(_context.AvalancheData);
            _context.Regions.RemoveRange(_context.Regions);
            await _context.SaveChangesAsync();
        }
    }
}
