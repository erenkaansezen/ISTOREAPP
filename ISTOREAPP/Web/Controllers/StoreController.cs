using ISTOREAPP.Data.Context;
using ISTOREAPP.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ISTOREAPP.Web.Controllers
{
    public class StoreController : Controller
    {
        private readonly StoreContext _context;

        public StoreController(StoreContext context)
        {
            _context = context;
        }
        public IActionResult StorePage(string category, int page = 1)
        {
            // Kategoriler listesini al
            var categories = _context.Categories.ToList();

            // Kategoriye göre ürünleri al
            var productsQuery = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                productsQuery = productsQuery.Where(p => p.Name == category); // Kategoriye göre filtreleme
            }

            // Sayfalama işlemi
            var pageSize = 10; // Sayfa başına ürün sayısı
            var totalItems = productsQuery.Count();
            var products = productsQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var pageInfo = new PageInfo
            {
                ItemsPerPage = pageSize,
                CurrentPage = page,
                TotalItems = totalItems
            };

            // ViewModel oluştur
            var viewModel = new StoreViewModel
            {
                products = products,
                Categories = categories,
                PageInfo = pageInfo,
                CurrentCategory = category
            };

            return View(viewModel);
        }




    }
}
