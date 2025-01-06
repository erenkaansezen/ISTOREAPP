using Business.Services;
using ISTOREAPP.Data.Entities;
using ISTOREAPP.Helpers;
using ISTOREAPP.Models;
using ISTOREAPP.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Web.Components;

public class FeaturedProductsViewComponent : ViewComponent
{
    private readonly ProductService _productService;

    public FeaturedProductsViewComponent(ProductService productService)
    {
        _productService = productService;
    }

    public async Task <IViewComponentResult> InvokeAsync()
    {
        var products = await _productService.GetAllProductsAsync();

        var model = new HomeViewModel
        {

            Products = products
        };

        return View(model);
    }
}
