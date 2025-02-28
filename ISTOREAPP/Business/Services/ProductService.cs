﻿using ISTOREAPP.Data.Context;
using ISTOREAPP.Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace Business.Services
{
    public class ProductService
    {
        private readonly StoreContext _context;

        public ProductService(StoreContext context)
        {
            _context = context;
        }


        /// Tüm ürünleri getirir.

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.Include(p => p.Categories).ToListAsync();
        }


        /// Belirli bir ID'ye sahip ürünü getirir.

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products
                                 .Include(p => p.Categories)
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }


        /// Belirli bir kategoriye ait ürünleri getirir.

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _context.Products
                                 .Where(p => p.Categories.Any(c => c.Id == categoryId))
                                 .ToListAsync();
        }

        public async Task AddProductCategoryAsync(ProductCategory productCategory)
        {
            await _context.ProductCategory.AddAsync(productCategory);
            await _context.SaveChangesAsync();
        }

        /// Yeni bir ürün ekler.

        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }


        /// Mevcut bir ürünü günceller.

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }


        /// Belirli bir ID'ye sahip ürünü siler.

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task IsActive (int id)
        {
            var products = _context.Products.FirstOrDefault(s => s.Id == id);


            products.IsActive = !products.IsActive; // Durumu tersine çevir
            await _context.SaveChangesAsync();
        }
        public async Task Top(int id)
        {
            var products = _context.Products.FirstOrDefault(s => s.Id == id);


            products.top = !products.top; // Durumu tersine çevir
            await _context.SaveChangesAsync();
        }
    }
}
