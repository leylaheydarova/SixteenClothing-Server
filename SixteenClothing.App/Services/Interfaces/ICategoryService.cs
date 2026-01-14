using SixteenClothing.App.ViewModels.Category;
using SixteenClothing.App.ViewModels.Pagination;

namespace SixteenClothing.App.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<PaginationViewModel<CategoryGetVM>> GetAllAsync(int page, int size);
        public Task<List<CategoryGetVM>> GetAllAsync();
        public Task<CategoryGetVM> GetSingleAsync(int id);
        public Task CreateAsync(CategoryCreateVM vm);
        public Task RemoveAsync(int id);
        public Task Update(CategoryUpdateVM vm);
    }
}
