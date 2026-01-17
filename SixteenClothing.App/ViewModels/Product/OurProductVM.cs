using SixteenClothing.App.ViewModels.Category;

namespace SixteenClothing.App.ViewModels.Product
{
    public class OurProductVM
    {
        public List<CategoryVM> Categories { get; set; } = new List<CategoryVM>();
        public List<ProductVM> Products { get; set; } = new List<ProductVM>();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int SelectedCategoryId { get; set; }
    }
}
