using ISTOREAPP.Data.Entities;

namespace ISTOREAPP.ViewModels
{
    public class StoreViewModel
    {


        public List<Category> Categories { get; set; }
        public List<Product>? products { get; set; }
        public int SelectedCategoryId { get; set; } // Kategori seçilen ID
        public PageInfo PageInfo { get; set; }
        public string CurrentCategory { get; set; } // Seçilen kategori
        public IEnumerable<ProductViewModel> Products { get; set; } = Enumerable.Empty<ProductViewModel>();
        public PageInfo PageInfos { get; set; } = new();
    }
}
