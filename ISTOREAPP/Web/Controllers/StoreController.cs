using Business.Services;
using ISTOREAPP.Data.Context;
using ISTOREAPP.Data.Entities;
using ISTOREAPP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISTOREAPP.Web.Controllers
{
    public class StoreController : Controller
    {
        private readonly StoreContext _context;
        private ProductService _productService;

        public StoreController(StoreContext context, ProductService productService)
        {
            _productService = productService;
            _context = context;
        }

        [Authorize]
        public IActionResult StorePage(string category, string searchTerm)
        {
            // Kategoriler listesini al
            var categories = _context.Categories.ToList();

            // Ürünleri kategoriye göre filtrele
            var productsQuery = _context.Products
                .Include(p => p.Categories)  // Categories ile ilişkiyi dahil et
                .AsQueryable();

            // Kategoriye göre filtreleme
            if (!string.IsNullOrEmpty(category))
            {
                productsQuery = productsQuery.Where(p => p.Categories.Any(c => c.Url == category));
            }

            // Arama terimine göre filtreleme (isime göre)
            if (!string.IsNullOrEmpty(searchTerm))
            {
                // EF.Functions.Like kullanarak LIKE benzeri arama yapıyoruz
                productsQuery = productsQuery.Where(p => EF.Functions.Like(p.Name, "%" + searchTerm + "%"));
            }

            // ViewModel oluştur
            var viewModel = new StoreViewModel
            {
                products = productsQuery.ToList(), // Filtrelenmiş ürünler
                Categories = categories,
                CurrentCategory = category
            };

            // Arama terimini ViewBag'e ekle
            ViewBag.SearchTerm = searchTerm;

            return View(viewModel);
        }

        public async Task<IActionResult> ProductDetails(int id)
        {
            var details = await _productService.GetProductByIdAsync(id);
            return View(details);
        }









    }
}
