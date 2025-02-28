﻿using ISTOREAPP.Data.Entities;
using ISTOREAPP.Models;

namespace ISTOREAPP.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<Category>? Categories { get; set; }
        public IEnumerable<Slider>? Sliders { get; set; }
    }
}
