using SixteenClothing.App.Areas.admin.ViewModels.Product;
using SixteenClothing.App.ViewModels.Pagination;
using SixteenClothing.App.ViewModels.Product;

namespace SixteenClothing.App.Services.Interfaces
{
    public interface IProductService : IService<ProductGetVM, ProductGetVM, ProductCreateVM, ProductUpdateVM>
    {
        public Task<PaginationViewModel<ProductVM>> GetAllAsync(int? categoryId, int page, int size);
    }
}
