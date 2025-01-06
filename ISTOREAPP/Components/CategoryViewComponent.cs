using Business.Services;
using ISTOREAPP.Data.Entities;
using ISTOREAPP.Helpers;
using ISTOREAPP.Models;
using ISTOREAPP.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Web.Components;

public class CategoryViewComponent : ViewComponent
{
    private readonly CategoryService _categoryService;

    public CategoryViewComponent(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task <IViewComponentResult> InvokeAsync()
    {
        var category = await _categoryService.GetAllCategoriesAsync();

        var model = new HomeViewModel
        {

            Categories = category
        };

        return View(model);
    }
}
