using SixteenClothing.App.Models;
using SixteenClothing.App.ViewModels.Category;
using SixteenClothing.App.ViewModels.Slider;

namespace SixteenClothing.App.Extensions
{
    public static class Mapping
    {
        public static SliderGetVM ToSliderGetVM(this Slider slider)
        {
            return new SliderGetVM
            {
                Id = slider.Id,
                Heading = slider.Heading,
                ImageName = slider.ImageName,
                ImageUrl = slider.ImageUrl,
                CreatedAt = slider.CreatedAt,
                Text = slider.Text,
                UpdatedAt = slider.UpdatedAt
            };
        }

        public static CategoryGetVM ToCategoryGetVM(this Category category)
        {
            return new CategoryGetVM
            {
                Id = category.Id,
                Name = category.Name,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt
            };
        }
    }
}
