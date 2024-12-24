using ISTOREAPP.Models;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Concrete;

namespace ISTOREAPP.ViewModels
{
    public class CategoryEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty; // Telefon => telefon || Beyaz Eşya => beyaz-esya

        public string? CategoryImg { get; set; }
        public bool IsActive { get; set; }

        public IEnumerable<Product>? products { get; set; }

    }
}
