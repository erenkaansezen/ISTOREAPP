using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTOREAPP.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty; // Telefon => telefon || Beyaz Eşya => beyaz-esya

        public string? CategoryImg { get; set; }
        public bool IsActive { get; set; }



        public List<Product> Products { get; set; } = new();
    }
    // productID CategoryId = 1 = 1
}
