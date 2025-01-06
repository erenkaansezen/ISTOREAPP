using Business.Services;
using ISTOREAPP.Data.Context;
using ISTOREAPP.Data.Entities;
using ISTOREAPP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISTOREAPP.Web.Controllers
{
    [Authorize(Roles = "Admin,Owner,Editör")]
    public class PageManagementController : Controller
    {
        private readonly StoreContext _context;
        private SliderService _sliderService;
        private readonly ProductService _productService;
        private ProductCategoryService _productCategoryService;

        public PageManagementController(StoreContext context, ProductService productService, ProductCategoryService productCategoryService, SliderService sliderService)
        {
            _context = context;
            _productService = productService;
            _productCategoryService = productCategoryService;
            _sliderService = sliderService;
        }



        //Mağaza Yönetimi
        public IActionResult StorePageManagement(string category)
        {
            // Kategoriler listesini al
            var categories = _context.Categories.ToList();

            // Ürünleri kategoriye göre filtrele
            var productsQuery = _context.Products
                .Include(p => p.Categories)  // Categories ile ilişkiyi dahil et
                .AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                productsQuery = productsQuery.Where(p => p.Categories.Any(c => c.Url == category));
            }

            // ViewModel oluştur
            var viewModel = new StoreViewModel
            {
                products = productsQuery.ToList(), // Filtrelenmiş ürünler
                Categories = categories,
                CurrentCategory = category
            };

            return View("StoreManagement/StorePageManagement", viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> StoreProductEdit(int id)
        {
            var products = await _productService.GetProductByIdAsync(id);
            return View("StoreManagement/StoreProductEdit", products);
        }
        public IActionResult StoreProductCreate(int id)
        {
            return View();
        }




        //Mağaza Yönetimi Product İşlemleri
        [HttpPost]
        public async Task<IActionResult> StoreProductEdit(int id, Data.Entities.Product model, ProductCategory categorymodel, IFormFile imageFile)
        {
            var categoriesid = _context.ProductCategory.FirstOrDefault();
            var products = await _productService.GetProductByIdAsync(id);
            // Yeni görsel yüklenmişse
            if (imageFile != null && imageFile.Length > 0)
            {
                // Dosya uzantısını al
                var extension = Path.GetExtension(imageFile.FileName);

                // Benzersiz dosya adı oluştur
                var randomFileName = $"{Guid.NewGuid()}{extension}";

                // Dosyanın kaydedileceği yolu oluştur
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Product", randomFileName);

                // Dosyayı belirtilen konuma kaydet
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // Ürüne yeni görsel yolunu ekle
                products.img = randomFileName;
            }
            else
            {
                model.img = products.img;
            }

            products.Name = model.Name;
            products.Features = model.Features;
            products.Description = model.Description;
            products.CategoryId = model.CategoryId;
            products.img = products.img;
            products.Price = model.Price;
            await _productService.UpdateProductAsync(products);


            // Eski ProductCategory kaydını sil
            await _productCategoryService.RemoveProductCategoryByProductIdAsync(products.Id);
            // Yeni CategoryId ile yeni bir ProductCategory oluştur
            var newProductCategory = new ProductCategory
            {
                ProductId = products.Id,
                CategoryId = model.CategoryId // Yeni CategoryId
            };

            // Yeni ProductCategory'yi ekle
            await _productCategoryService.AddProductCategoryAsync(newProductCategory);

            return RedirectToAction("StorePageManagement");
        }

        [HttpPost]
        public async Task<IActionResult> StoreProductCreate(Data.Entities.Product productmodel, ProductCategory categorymodel, IFormFile imageFile)
        {
            if (imageFile != null)
            {
                // Dosya uzantısını al
                var extension = Path.GetExtension(imageFile.FileName);

                // Benzersiz dosya adı oluştur
                var randomFileName = $"{Guid.NewGuid()}{extension}";

                // Dosyanın kaydedileceği yolu oluştur
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Product", randomFileName);

                // Dosyayı belirtilen konuma kaydet
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // Slider modeline görsel adını ata
                productmodel.img = randomFileName;
            }

            if (productmodel.img != null)
            {
                var product = new Data.Entities.Product()
                {
                    Name = productmodel.Name,
                    Description = productmodel.Description,
                    Price = productmodel.Price,
                    CategoryId = productmodel.CategoryId,
                    img = productmodel.img,
                    IsActive = productmodel.IsActive,
                };
                await _productService.AddProductAsync(product);

                var ProdcutCategory = new ProductCategory()
                {
                    CategoryId = productmodel.CategoryId,
                    ProductId = product.Id,
                };
                await _context.ProductCategory.AddAsync(ProdcutCategory);


                await _context.SaveChangesAsync();


            }
            return RedirectToAction("HomeSliderManagement");

        }
        [HttpPost]
        public async Task<IActionResult> StoreProductDelete(int id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction("StorePageManagement");

        }
        [HttpPost]
        public async Task<IActionResult> StoreIsActive(int id)
        {
            await _productService.IsActive(id);
            return RedirectToAction("StorePageManagement");

        }
        [HttpPost]
        public async Task<IActionResult> StoreTop(int id)
        {
            await _productService.Top(id);
            return RedirectToAction("StorePageManagement");

        }





        //Anasayfa Yönetimi 
        public IActionResult HomePageManagement()
        {
            return View("HomeManagement/HomePageManagement");
        }
        public IActionResult HomeSliderManagement()
        {
            // Veritabanından Slider verilerini al
            var sliders = _context.Sliders.ToList();
            // Dönüştürülen modeli view'a gönder
            return View("HomeManagement/HomeSliderManagement", sliders);

        }
        public IActionResult HomeCategoryManagement()
        {
            var categories = _context.Categories.ToList();
            return View("HomeManagement/HomeCategoryManagement", categories);
        }
        public IActionResult HomeFeaturedManagement()
        {
            return View("HomeManagement/HomeFeaturedManagement");
        }


        //Slider Yönetimi
        [HttpGet]
        public async Task<IActionResult> SliderEdit(int id)
        {
            var Sliders = await _sliderService.GetSliderByIdAsync(id);
            return View("SliderManagement/SliderEdit", Sliders);
        }
        [HttpPost]
        public async Task<IActionResult> SliderEdit(int id, Slider slidermodel, IFormFile imageFile)
        {
            var Sliders = await _sliderService.GetSliderByIdAsync(id);

            Sliders.SliderImgName = slidermodel.SliderImgName;


            if (imageFile != null && imageFile.Length > 0)
            {
                // Dosya uzantısını al
                var extension = Path.GetExtension(imageFile.FileName);

                // Benzersiz dosya adı oluştur
                var randomFileName = $"{Guid.NewGuid()}{extension}";

                // Dosyanın kaydedileceği yolu oluştur
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Slider", randomFileName);

                // Dosyayı belirtilen konuma kaydet
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // Ürüne yeni görsel yolunu ekle
                Sliders.SliderImg = randomFileName;
            }
            else
            {
                Sliders.SliderImg = slidermodel.SliderImg;
            }


            

            await _sliderService.UpdateSliderAsync(Sliders);
            return RedirectToAction("HomeSliderManagement");

        }

        [HttpPost]
        public IActionResult SliderActive(int id)
        {
            var slider = _context.Sliders.FirstOrDefault(s => s.Id == id);
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
            var slider = _context.Sliders.FirstOrDefault(s => s.Id == id);
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
            return View("SliderManagement/SliderCreate");
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
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Slider", randomFileName);

                // Dosyayı belirtilen konuma kaydet
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // Slider modeline görsel adını ata
                model.SliderImg = randomFileName;
            }

            if (model.Id == 0)
            {
                // Yeni slider ekleme işlemi
                model.Id = _context.Sliders.Count() + 1; // Yeni ID oluştur
                _context.Sliders.Add(model);
            }
            else
            {
                // Mevcut slider'ı güncelleme işlemi
                var existingSlider = _context.Sliders.FirstOrDefault(s => s.Id == model.Id);
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
            return RedirectToAction("HomeManagement/HomeSliderManagement");
        }


        //Kategori Sayfası Yönetimi
        [HttpPost]
        public IActionResult CategoryEdit(int id)
        {
            var category = _context.Categories.FirstOrDefault(s => s.Id == id);
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
            return View("CategoryManagement/CategoryCreate");
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
            return RedirectToAction("HomeManagement/HomeCategoryManagement");
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

        //İletişim Sayfası Yönetimi
        public IActionResult ContactPageManagement()
        {
            return View();
        }


    }
}
