using Business.Services;
using ISTOREAPP.Data.Entities;
using ISTOREAPP.Helpers;
using ISTOREAPP.Models;
using ISTOREAPP.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Web.Components;

public class SliderViewComponent : ViewComponent
{
    private readonly SliderService _sliderService;

    public SliderViewComponent(SliderService sliderService)
    {
        _sliderService = sliderService;
    }

    public async Task <IViewComponentResult> InvokeAsync()
    {
        var sliders = await _sliderService.GetAllSlidersAsync();

        var model = new HomeViewModel
        {

            Sliders = sliders
        };

        return View(model);
    }
}
