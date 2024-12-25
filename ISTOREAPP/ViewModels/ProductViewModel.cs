using ISTOREAPP.Data.Entities;

namespace ISTOREAPP.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal? Price { get; set; }
        public int CategoryId { get; set; }  // Kategoriyi belirten ID

        public string img { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public List<Product> products { get; set; }

        public List<Category> Categories { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
    public class ProductListViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; } = Enumerable.Empty<ProductViewModel>();
        public List<Product> products { get; set; }


    }
}
