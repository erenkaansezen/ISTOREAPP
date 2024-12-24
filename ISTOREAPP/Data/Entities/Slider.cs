using System.ComponentModel.DataAnnotations;

namespace ISTOREAPP.Data.Entities
{
    public class Slider
    {
        [Key]
        public int SliderImgId { get; set; }
        public string? SliderImgName { get; set; }
        public string? SliderImg { get; set; }

        public bool IsActive { get; set; }
    }
}
