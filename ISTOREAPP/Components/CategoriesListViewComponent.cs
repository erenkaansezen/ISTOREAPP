using ISTOREAPP.Models;
using ISTOREAPP.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Concrete;

namespace ISTOREAPP.Components
{
    public class CategoriesListViewComponent : ViewComponent
    {
        private readonly StoreContext _db;

        public CategoriesListViewComponent(StoreContext context)
        {
            _db = context;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(_db.Categories.Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Url = c.Url
            }).ToList());
        }
    }
}
