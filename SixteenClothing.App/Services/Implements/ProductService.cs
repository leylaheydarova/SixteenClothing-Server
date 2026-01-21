using Microsoft.EntityFrameworkCore;
using SixteenClothing.App.Areas.admin.ViewModels.Product;
using SixteenClothing.App.Constants;
using SixteenClothing.App.Contexts;
using SixteenClothing.App.Extensions;
using SixteenClothing.App.Models;
using SixteenClothing.App.Services.Interfaces;
using SixteenClothing.App.ViewModels.Pagination;
using SixteenClothing.App.ViewModels.Product;

namespace SixteenClothing.App.Services.Implements
{
    public class ProductService : IProductService
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;
        readonly IHttpContextAccessor _accessor;

        public ProductService(AppDbContext context, IWebHostEnvironment env, IHttpContextAccessor accessor)
        {
            _context = context;
            _env = env;
            _accessor = accessor;
        }

        public async Task CreateAsync(ProductCreateVM vm)
        {
            var category = await _context.Categories.FindAsync(vm.CategoryId);
            if (category == null) throw new Exception("Category is not found");
            var product = new Product
            {
                CategoryId = category.Id,
                Description = vm.Description,
                Name = vm.Name,
                Price = vm.Price,
                CreatedAt = TimeZones.AzerbaijaniTime
            };
            product.ImageName = await vm.Image.UploadFile(_env.WebRootPath, FilePaths.ProductPath);
            product.ImageUrl = $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}/{FilePaths.ProductPath}/{product.ImageName}";
            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginationViewModel<ProductGetVM>> GetAllAsync(int page, int size)
        {
            var totalPages = (int)Math.Ceiling((double)(await _context.Products.CountAsync()) / size);
            var query = await _context.Products.AsNoTracking().OrderByDescending(c => c.CreatedAt)
                .Skip((page - 1) * size)
                .Take(size)
                .Select(product => product.ToProductGetVM()).ToListAsync();
            return new PaginationViewModel<ProductGetVM>(query, totalPages, page, size);
        }

        public async Task<List<ProductGetVM>> GetAllAsync()
        {
            return await _context.Products.AsNoTracking().Select(product => product.ToProductGetVM()).ToListAsync();
        }

        public async Task<PaginationViewModel<ProductVM>> GetAllAsync(int? categoryId, int page, int size)
        {
            var totalPages = (await _context.Products.CountAsync()) / size;
            var products = _context.Products.AsNoTracking().AsQueryable();
            if (categoryId.HasValue) products = products.Where(p => p.CategoryId == categoryId);
            var query = await products
                .Skip((page - 1) * size)
                .Take(size)
                .Select(product => new ProductVM()
                {
                    Id = product.Id,
                    Description = product.Description,
                    ImageUrl = product.ImageUrl,
                    Name = product.Name,
                    Price = product.Price
                }).ToListAsync();
            return new PaginationViewModel<ProductVM>(query, totalPages, page, size);
        }

        public async Task<ProductGetVM> GetSingleAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) throw new Exception("Product is not found!");
            return product.ToProductGetVM();
        }

        public async Task RemoveAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) throw new Exception("Product is not found!");
            var path = $"{_env.WebRootPath}/{FilePaths.ProductPath}/{product.ImageName}";
            if (File.Exists(path)) File.Delete(path);
            _context.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductUpdateVM vm)
        {
            var product = await _context.Products.FindAsync(vm.Id);
            if (product == null) throw new Exception("Product is not found!");

            product.Name = !string.IsNullOrEmpty(vm.Name) ? vm.Name : product.Name;
            product.Description = !string.IsNullOrEmpty(vm.Description) ? vm.Description : product.Description;
            product.Price = vm.Price.HasValue ? vm.Price.Value : product.Price;
            if (vm.Image != null)
            {
                var path = $"{FilePaths.ProductPath}/{product.ImageName}";
                if (File.Exists(path)) File.Delete(path);
                product.ImageName = await vm.Image.UploadFile(_env.WebRootPath, FilePaths.ProductPath);
                product.ImageUrl = $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}/{FilePaths.ProductPath}/{product.ImageName}";
            }
            product.UpdatedAt = TimeZones.AzerbaijaniTime;

            _context.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
