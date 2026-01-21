using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SixteenClothing.App.Areas.admin.ViewModels.Category;
using SixteenClothing.App.Areas.admin.ViewModels.Product;
using SixteenClothing.App.Areas.admin.ViewModels.Slider;
using SixteenClothing.App.Contexts;
using SixteenClothing.App.Models;
using SixteenClothing.App.Services.Implements;
using SixteenClothing.App.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(opt =>
opt.UseNpgsql(builder.Configuration["ConnectionStrings:Default"]));

builder.Services.AddIdentity<AppUser, IdentityRole<int>>(options =>
{
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.Password.RequireDigit = true;
    options.Password.RequiredUniqueChars = 1;

    options.User.RequireUniqueEmail = true;

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.AllowedForNewUsers = true;

    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.SignIn.RequireConfirmedEmail = false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

//Services
builder.Services.AddScoped<IService<SliderGetVM, SliderGetVM, SliderCreateVM, SliderUpdateVM>, SliderService>();
builder.Services.AddScoped<IService<CategoryGetVM, CategoryGetVM, CategoryCreateVM, CategoryUpdateVM>, CategoryService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IService<ProductGetVM, ProductGetVM, ProductCreateVM, ProductUpdateVM>, ProductService>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
