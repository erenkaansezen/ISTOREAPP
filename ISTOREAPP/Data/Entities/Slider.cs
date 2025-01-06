using System.ComponentModel.DataAnnotations;

namespace ISTOREAPP.Data.Entities
{
    public class Slider
    {
        [Key]
        public int SliderImgId { get; set; }
        public string SliderImgName { get; set; } = string.Empty;
        public string SliderImg { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}
