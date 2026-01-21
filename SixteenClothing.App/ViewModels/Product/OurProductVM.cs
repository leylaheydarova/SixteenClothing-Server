using SixteenClothing.App.ViewModels.Pagination;

namespace SixteenClothing.App.ViewModels.Product
{
    public class OurProductVM
    {
        public PaginationViewModel<ProductVM> Products { get; set; }
        public int CurrentPage { get; set; }
        public int? SelectedCategoryId { get; set; }
        public int TotalPages { get { return Products.TotalPages; } }
    }
}
