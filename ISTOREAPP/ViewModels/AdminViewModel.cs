using ISTOREAPP.Models;

namespace ISTOREAPP.ViewModels
{
    public class AdminViewModel
    {
        public IEnumerable<AppRole> Roles { get; set; }
        public IEnumerable<Slider> Sliders { get; set; }
    }
}
