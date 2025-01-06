using ISTOREAPP.Data.Context;
using ISTOREAPP.Data.Entities;
using Iyzipay.Model.V2.Subscription;
using Microsoft.EntityFrameworkCore;


namespace Business.Services
{
    public class ProductCategoryService
    {
        private readonly StoreContext _context;

        public ProductCategoryService(StoreContext context)
        {
            _context = context;
        }


        /// Tüm ürünleri getirir.

        // Ürün ve kategori ilişkisini almak
        public async Task<ProductCategory?> GetProductCategoryByProductIdAndCategoryIdAsync(int productId, int categoryId)
        {
            return await _context.ProductCategory
                .FirstOrDefaultAsync(pc => pc.ProductId == productId && pc.CategoryId == categoryId);

        }

        // Yeni bir ProductCategory eklemek
        public async Task AddProductCategoryAsync(ProductCategory productCategory)
        {
            await _context.ProductCategory.AddAsync(productCategory);
            await _context.SaveChangesAsync();

        }

        // Mevcut ProductCategory'yi güncellemek
        public async Task UpdateProductCategoryAsync(ProductCategory productCategory)
        {
            _context.ProductCategory.Update(productCategory);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveProductCategoryByProductIdAsync(int productId)
        {
            // Ürüne ait ilişkiyi bul
            var productCategory = await _context.ProductCategory
                .FirstOrDefaultAsync(pc => pc.ProductId == productId);

            if (productCategory != null)
            {
                // ProductCategory kaydını sil
                _context.ProductCategory.Remove(productCategory);
                await _context.SaveChangesAsync();
            }


        }
    }
}

