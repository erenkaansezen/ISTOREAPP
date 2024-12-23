using ISTOREAPP.Models;
using StoreApp.Data.Concrete;

namespace ISTOREAPP.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Category>? Categories { get; set; }
        public IEnumerable<Slider>? Sliders { get; set; }
    }
}
