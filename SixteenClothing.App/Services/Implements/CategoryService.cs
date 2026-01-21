using Microsoft.EntityFrameworkCore;
using SixteenClothing.App.Areas.admin.ViewModels.Category;
using SixteenClothing.App.Constants;
using SixteenClothing.App.Contexts;
using SixteenClothing.App.Extensions;
using SixteenClothing.App.Models;
using SixteenClothing.App.Services.Interfaces;
using SixteenClothing.App.ViewModels.Pagination;

namespace SixteenClothing.App.Services.Implements
{
    public class CategoryService : ICategoryService
    {
        readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CategoryCreateVM vm)
        {
            var category = new Category()
            {
                Name = vm.Name,
                CreatedAt = TimeZones.AzerbaijaniTime
            };
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Category>> GetAllEntitiesAsync()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<PaginationViewModel<CategoryGetVM>> GetAllAsync(int page, int size)
        {
            var totalPages = (await _context.Categories.CountAsync()) / size;
            var query = await _context.Categories.AsNoTracking().OrderByDescending(c => c.CreatedAt)
                .Skip((page - 1) * size)
                .Take(size)
                .Select(category => category.ToCategoryGetVM()).ToListAsync();
            return new PaginationViewModel<CategoryGetVM>(query, totalPages, page, size);
        }

        public async Task<List<CategoryGetVM>> GetAllAsync()
        {
            return await _context.Categories.AsNoTracking().Select(category => category.ToCategoryGetVM()).ToListAsync();
        }

        public async Task<CategoryGetVM> GetSingleAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) throw new Exception("Category was not found!");
            return category.ToCategoryGetVM();
        }

        public async Task RemoveAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) throw new Exception("Category was not found!");
            _context.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CategoryUpdateVM vm)
        {
            var category = await _context.Categories.FindAsync(vm.Id);
            if (category == null) throw new Exception("Category was not found!");
            category.Name = vm.Name != null ? vm.Name : category.Name;
            category.UpdatedAt = TimeZones.AzerbaijaniTime;
            _context.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
