using Microsoft.AspNetCore.Mvc;

namespace ISTOREAPP.Models
{
    public class MapperProfile : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
