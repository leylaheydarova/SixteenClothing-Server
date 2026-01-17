using SixteenClothing.App.ViewModels.Pagination;

namespace SixteenClothing.App.Services.Interfaces
{
    public interface IService<TGetAllVM, TGetSingleVM, TCreateVM, TUpdateVM> where TGetAllVM : class
    {
        public Task<PaginationViewModel<TGetAllVM>> GetAllAsync(int page, int size);
        public Task<List<TGetAllVM>> GetAllAsync();
        public Task<TGetSingleVM> GetSingleAsync(int id);
        public Task CreateAsync(TCreateVM vm);
        public Task RemoveAsync(int id);
        public Task UpdateAsync(TUpdateVM vm);
    }
}
