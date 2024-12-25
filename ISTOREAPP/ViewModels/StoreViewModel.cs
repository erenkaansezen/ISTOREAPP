using ISTOREAPP.Data.Entities;

namespace ISTOREAPP.ViewModels
{
    public class StoreViewModel
    {


        public List<Category> Categories { get; set; }
        public List<Product> products { get; set; }
        public int SelectedCategoryId { get; set; } // Kategori seçilen ID

        public string CurrentCategory { get; set; } // Seçilen kategori

    }
}
