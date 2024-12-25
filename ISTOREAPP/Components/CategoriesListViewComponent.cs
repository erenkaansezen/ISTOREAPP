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

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(_storeRepository.Categories.Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Url = c.Url
            }).ToList());
        }
    }
}
