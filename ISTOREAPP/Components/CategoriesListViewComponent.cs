using ISTOREAPP.Data.Context;
using ISTOREAPP.Data.Entities;

using ISTOREAPP.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISTOREAPP.Components
{
    public class CategoriesListViewComponent : ViewComponent
    {
        private readonly StoreContext _storeRepository;


        public CategoriesListViewComponent(StoreContext storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public IViewComponentResult Invoke(string viewName)
        {
            // Eğer viewName null veya boşsa, varsayılan olarak "default" kullan
            viewName = string.IsNullOrEmpty(viewName) ? "default" : viewName;

            // ViewBag.SelectedCategory'yi RouteData'dan al
            ViewBag.SelectedCategory = RouteData?.Values["category"];

            // Kategorileri al ve View'a ilet
            var categories = _storeRepository.Categories.Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Url = c.Url
            }).ToList();

            // Dinamik olarak belirlenen view'ı render et
            return View(viewName, categories);
        }
    }
}
