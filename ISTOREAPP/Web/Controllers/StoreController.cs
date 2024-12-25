using ISTOREAPP.Data.Context;
using ISTOREAPP.Data.Entities;
using ISTOREAPP.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISTOREAPP.Web.Controllers
{
    public class StoreController : Controller
    {
        private readonly StoreContext _context;

        public StoreController(StoreContext context)
        {
            _context = context;
        }
        public IActionResult StorePage(string category)
        {
            // Kategoriler listesini al
            var categories = _context.Categories.ToList();

            // Ürünleri kategoriye göre filtrele
            var productsQuery = _context.Products
                .Include(p => p.Categories)  // Categories ile ilişkiyi dahil et
                .AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                productsQuery = productsQuery.Where(p => p.Categories.Any(c => c.Url == category));
            }

            // ViewModel oluştur
            var viewModel = new StoreViewModel
            {
                products = productsQuery.ToList(), // Filtrelenmiş ürünler
                Categories = categories,
                CurrentCategory = category
            };

            return View(viewModel);
        }





    }
}
