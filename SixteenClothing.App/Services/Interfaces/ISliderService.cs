using SixteenClothing.App.ViewModels.Pagination;
using SixteenClothing.App.ViewModels.Slider;

namespace SixteenClothing.App.Services.Interfaces
{
    public interface ISliderService
    {
        Task<PaginationViewModel<SliderGetVM>> GetAllAsync(int page, int size);
        Task<List<SliderGetVM>> GetAllAsync();
        Task<SliderGetVM> GetSingleAsync(int id);
        Task CreateAsync(SliderCreateVM vm);
        Task RemoveAsync(int id);
        Task UpdateAsync(SliderUpdateVM vm);
    }
}
