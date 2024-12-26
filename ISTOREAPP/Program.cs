using Business.Services;
using ISTOREAPP.Business.Identity;
using ISTOREAPP.Data.Context;
using ISTOREAPP.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<SliderService>(); // SliderService eklenmiþ
builder.Services.AddScoped<CategoryService>(); // CategoryService eklenmiþ
builder.Services.AddScoped<ProductService>(); // ProductService eklenmiþ


builder.Services.AddDbContext<StoreContext>(options =>
    options.UseSqlite(builder.Configuration["ConnectionStrings:Dbconnection"]));

builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<StoreContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/User/Login";
    options.AccessDeniedPath = "/User/AccessDenied";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
});
//builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



// products/telefon => kategori urun listesi
app.MapControllerRoute("products_in_category", "StorePage/{category?}", new { controller = "Store", action = "StorePage" });
app.MapControllerRoute("products_in_category", "StorePageManagement/{category?}", new { controller = "PageManagement", action = "StorePageManagement" });


app.MapDefaultControllerRoute();


IdentitySeedData.IdentityTestUser(app);

app.Run();

