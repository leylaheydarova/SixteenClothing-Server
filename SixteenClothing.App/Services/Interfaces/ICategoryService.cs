using SixteenClothing.App.Areas.admin.ViewModels.Category;
using SixteenClothing.App.Models;

namespace SixteenClothing.App.Services.Interfaces
{
    public interface ICategoryService : IService<CategoryGetVM, CategoryGetVM, CategoryCreateVM, CategoryUpdateVM>
    {
        public Task<ICollection<Category>> GetAllEntitiesAsync();
    }
}
