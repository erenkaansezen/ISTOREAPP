using ISTOREAPP.Models;
using ISTOREAPP.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Concrete;

namespace ISTOREAPP.Controllers
{
    public class PageManagementController : Controller
    {
        private readonly StoreContext _context;

        public PageManagementController(StoreContext context)
        {
            _context = context;
        }
        public IActionResult HomePageManagement()
        {
            return View();
        }
        public IActionResult StorePageManagement()
        {
            var products = _context.Products.ToList();
            return View(products);
        }
        public IActionResult ContactPageManagement()
        {
            return View();
        }


        //ANASAYFA CONTROLLERS!
        public IActionResult HomeSliderManagement()
        {
            // Veritabanından Slider verilerini al
            var sliders = _context.Sliders.ToList();



            // Dönüştürülen modeli view'a gönder
            return View(sliders);
        }
        public IActionResult HomeCategoryManagement()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }
        public IActionResult HomeFeaturedManagement()
        {
            return View();
        }

        //Slider Edit
        [HttpPost]
        public IActionResult SliderEdit(int id)
        {
            var slider = _context.Sliders.FirstOrDefault(s => s.SliderImgId == id);
            if (slider == null)
            {
                return NotFound();
            }

            slider.IsActive = !slider.IsActive; // Durumu tersine çevir
            _context.SaveChanges();
            return RedirectToAction("HomeSliderManagement");
        }

        [HttpPost]
        public IActionResult SliderDelete(int id)
        {
            var slider = _context.Sliders.FirstOrDefault(s => s.SliderImgId == id);
            if (slider == null)
            {
                return NotFound();
            }

            _context.Sliders.Remove(slider); // Durumu tersine çevir
            _context.SaveChanges();
            return RedirectToAction("HomeSliderManagement");
        }

        public IActionResult SliderCreate(int id)
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SliderCreate(Slider model, IFormFile imageFile)
        {
            if (imageFile != null)
            {
                // Dosya uzantısını al
                var extension = Path.GetExtension(imageFile.FileName);

                // Benzersiz dosya adı oluştur
                var randomFileName = $"{Guid.NewGuid()}{extension}";

                // Dosyanın kaydedileceği yolu oluştur
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);

                // Dosyayı belirtilen konuma kaydet
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // Slider modeline görsel adını ata
                model.SliderImg = randomFileName;
            }

            if (model.SliderImgId == 0)
            {
                // Yeni slider ekleme işlemi
                model.SliderImgId = _context.Sliders.Count() + 1; // Yeni ID oluştur
                _context.Sliders.Add(model);
            }
            else
            {
                // Mevcut slider'ı güncelleme işlemi
                var existingSlider = _context.Sliders.FirstOrDefault(s => s.SliderImgId == model.SliderImgId);
                if (existingSlider != null)
                {
                    existingSlider.SliderImg = model.SliderImg;
                    existingSlider.SliderImgName = model.SliderImgName;
                    existingSlider.IsActive = model.IsActive;
                }
            }

            // Değişiklikleri kaydet
            await _context.SaveChangesAsync();

            // Listeleme sayfasına yönlendir
            return RedirectToAction("HomeSliderManagement");
        }

        [HttpPost]
        public IActionResult CategoryEdit(int id)
        {
            var category= _context.Categories.FirstOrDefault(s => s.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            category.IsActive = !category.IsActive; // Durumu tersine çevir
            _context.SaveChanges();
            return RedirectToAction("HomeCategoryManagement");
        }
        public IActionResult CategoryCreate(int id)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CategoryCreate(Category model, IFormFile imageFile)
        {
            if (imageFile != null)
            {
                // Dosya uzantısını al
                var extension = Path.GetExtension(imageFile.FileName);

                // Benzersiz dosya adı oluştur
                var randomFileName = $"{Guid.NewGuid()}{extension}";

                // Dosyanın kaydedileceği yolu oluştur
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Category", randomFileName);

                // Dosyayı belirtilen konuma kaydet
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // Slider modeline görsel adını ata
                model.CategoryImg = randomFileName;
            }

            if (model.Id == 0)
            {
                // Yeni slider ekleme işlemi
                model.Id = _context.Categories.Count() + 1; // Yeni ID oluştur
                _context.Categories.Add(model);
            }
            else
            {
                // Mevcut slider'ı güncelleme işlemi
                var existingCategory = _context.Categories.FirstOrDefault(s => s.Id == model.Id);
                if (existingCategory != null)
                {
                    existingCategory.CategoryImg = model.CategoryImg;
                    existingCategory.Name = model.Name;
                    existingCategory.Url = model.Url;
                    existingCategory.IsActive = model.IsActive;
                }
                // Fotoğraf varsa, güncelle
                if (!string.IsNullOrEmpty(model.CategoryImg))
                {
                    existingCategory.CategoryImg = model.CategoryImg;
                }
            }

            // Değişiklikleri kaydet
            await _context.SaveChangesAsync();

            // Listeleme sayfasına yönlendir
            return RedirectToAction("HomeCategoryManagement");
        }


        [HttpPost]
        public IActionResult CategoryDelete(int id)
        {
            var category = _context.Categories.FirstOrDefault(s => s.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category); // Durumu tersine çevir
            _context.SaveChanges();
            return RedirectToAction("HomeCategoryManagement");
        }
    }
}
