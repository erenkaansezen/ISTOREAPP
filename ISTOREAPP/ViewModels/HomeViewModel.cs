using ISTOREAPP.Data.Entities;

namespace ISTOREAPP.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Category>? Categories { get; set; }
        public IEnumerable<Slider>? Sliders { get; set; }
    }
}
