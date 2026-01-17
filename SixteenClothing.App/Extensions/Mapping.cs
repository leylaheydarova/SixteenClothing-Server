using SixteenClothing.App.Areas.admin.ViewModels.Category;
using SixteenClothing.App.Areas.admin.ViewModels.Product;
using SixteenClothing.App.Areas.admin.ViewModels.Slider;
using SixteenClothing.App.Models;

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

        public static ProductGetVM ToProductGetVM(this Product product)
        {
            return new ProductGetVM
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }

    }
}
