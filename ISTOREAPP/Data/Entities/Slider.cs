using System.ComponentModel.DataAnnotations;

namespace ISTOREAPP.Data.Entities
{
    public class Slider
    {

        public int Id { get; set; }
        public string SliderImgName { get; set; } = string.Empty;
        public string SliderImg { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}
