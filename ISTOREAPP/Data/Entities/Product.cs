﻿using Microsoft.EntityFrameworkCore;

namespace ISTOREAPP.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string Features { get; set; } = string.Empty;
        public double? Price { get; set; }
        public int CategoryId { get; set; }  // Kategoriyi belirten ID

        public string img { get; set; } = string.Empty;

        

        public bool IsActive { get; set; }

        public bool top { get; set; }

        public List<Category> Categories { get; set; } = new();
        public ICollection<ProductCategory> ProductCategories { get; set; }



    }
}
