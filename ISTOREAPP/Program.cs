using Business.Services;
using ISTOREAPP.Business.Identity;
using ISTOREAPP.Data.Context;
using ISTOREAPP.Data.Entities;
using ISTOREAPP.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddScoped<SliderService>(); // SliderService eklenmi�
builder.Services.AddScoped<CategoryService>(); // CategoryService eklenmi�
builder.Services.AddScoped<ProductService>(); // ProductService eklenmi�
builder.Services.AddScoped<OrderService>(); // Order eklenmi�

builder.Services.AddScoped<Cart>();
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

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

builder.Services.AddDistributedMemoryCache(); // Session'� Memory �zerinde Kullan�r

builder.Services.AddSession(); // Session'� Aktif Eder



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



//Routing
app.MapRazorPages();
app.MapRazorPages().WithMetadata(new { Name = "Cart", Route = "/Cart" });

app.MapControllerRoute("products_in_category", "/StorePage", new { controller = "Store", action = "StorePage" });

app.MapControllerRoute("products_in_category", "StorePage/{category?}", new { controller = "Store", action = "StorePage" });
app.MapControllerRoute("products_in_category", "StorePageManagement/{category?}", new { controller = "PageManagement", action = "StorePageManagement" });
app.MapDefaultControllerRoute();



IdentitySeedData.IdentityTestUser(app);

app.Run();

