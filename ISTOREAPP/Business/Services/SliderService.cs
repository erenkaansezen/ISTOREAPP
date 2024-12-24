using ISTOREAPP.Data.Context;
using ISTOREAPP.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public class SliderService
    {
        private readonly StoreContext _context;

        public SliderService(StoreContext context)
        {
            _context = context;
        }

        // Tüm Slider'ları Getir
        public async Task<IEnumerable<Slider>> GetAllSlidersAsync()
        {
            return await _context.Sliders.ToListAsync();
        }

        // ID'ye Göre Slider Getir
        public async Task<Slider> GetSliderByIdAsync(int id)
        {
            return await _context.Sliders.FindAsync(id);
        }

        // Yeni Slider Ekle
        public async Task AddSliderAsync(Slider slider)
        {
            _context.Sliders.Add(slider);
            await _context.SaveChangesAsync();
        }

        // Slider Güncelle
        public async Task UpdateSliderAsync(Slider slider)
        {
            _context.Sliders.Update(slider);
            await _context.SaveChangesAsync();
        }

        // Slider Sil
        public async Task DeleteSliderAsync(int id)
        {
            var slider = await _context.Sliders.FindAsync(id);
            if (slider != null)
            {
                _context.Sliders.Remove(slider);
                await _context.SaveChangesAsync();
            }
        }
    }
}
