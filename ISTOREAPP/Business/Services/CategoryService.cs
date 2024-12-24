using ISTOREAPP.Data.Context;
using ISTOREAPP.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class CategoryService
    {
        private readonly StoreContext _context;

        public CategoryService(StoreContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Tüm kategorileri getirir.
        /// </summary>
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        /// Belirli bir ID'ye sahip kategoriyi getirir.
        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        /// Yeni bir kategori ekler.
        public async Task AddCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        /// Mevcut bir kategoriyi günceller.
        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        /// Belirli bir ID'ye sahip kategoriyi siler.
        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
