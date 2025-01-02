using ISTOREAPP.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ISTOREAPP.Data.Context
{
    public class StoreContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();

        public DbSet<Slider> Sliders => Set<Slider>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<ProductCategory> ProductCategory => Set<ProductCategory>();








        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Categories)
                .WithMany(e => e.Products)
                .UsingEntity<ProductCategory>();

            modelBuilder.Entity<Category>() //kategorilerin uniq yani benzersiz olmasını sağlar
                .HasIndex(u => u.Url)
                .IsUnique();

            modelBuilder.Entity<Product>().HasData
                (
                     new List<Product>()
                     {
                    new() {Id=1, Name="Samsung S24", Price=5000,Description="Güzel Telefon" , img = "telefon.jpg",IsActive=true},
                    new() {Id=2, Name="Samsung S25", Price=6000,Description="Güzel Telefon" , img = "telefon.jpg",IsActive=true},
                    new() {Id=3, Name="Samsung S26", Price=7000,Description="Güzel Telefon", img = "telefon.jpg",IsActive=true},
                    new() {Id=4, Name="Samsung S27", Price=8000,Description="Güzel Telefon",img="telefon.jpg", IsActive = true},
                    new() {Id=5, Name="Macbook Pro", Price=9000,Description="Güzel Telefon",img="telefon.jpg", IsActive = true},
                    new() {Id=6, Name="Macbook Air", Price=10000,Description="Güzel Telefon",img="telefon.jpg", IsActive = true},
                     }
                );
            modelBuilder.Entity<Category>().HasData(
                    new List<Category>()
                    {
                    new() {Id=1,Name="Telefon",Url="telefon",CategoryImg="telefon.jpg" },
                    new() {Id=2,Name="Bilgisayar",Url="bilgisayar",CategoryImg ="computer.jpg"},
                    new() {Id=3,Name="Ekipman",Url="ekipman",CategoryImg="ekipman.jpg"} // extension method, slug dotnet
                    }
                );

            modelBuilder.Entity<ProductCategory>().HasData(
                    new List<ProductCategory>()
                    {
                    new ProductCategory() {ProductId = 1, CategoryId =1},
                    new ProductCategory() {ProductId = 2, CategoryId =1},
                    new ProductCategory() {ProductId = 3, CategoryId =1},
                    new ProductCategory() {ProductId = 4, CategoryId =1},

                    new ProductCategory() {ProductId = 5, CategoryId =2},
                    new ProductCategory() {ProductId = 6, CategoryId =2},


                        // ıd kesişimleri uniq olmalıdır
                    }
                );
            //modelBuilder.Entity<Slider>().HasData(
            //        new List<Slider>()
            //        {
            //        new() {SliderImgId=1,SliderImgName="Indırım1",SliderImg="Indırım1.jpg",IsActive=true },
            //        new() {SliderImgId=2,SliderImgName="Indırım2",SliderImg="Indırım2.jpg",IsActive=true },
            //        new() {SliderImgId=3,SliderImgName="Indırım3",SliderImg="Indırım3.jpg",IsActive=true },
            //        }
            //    );

        }









    }
}
