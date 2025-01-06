using Business.Services;
using ISTOREAPP.Data.Entities;
using ISTOREAPP.Helpers;
using ISTOREAPP.Models;
using ISTOREAPP.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Web.Components;

public class ProductBannerViewComponent : ViewComponent
{

    public   IViewComponentResult Invoke()
    {
        return View();
    }
}
