namespace SixteenClothing.App.Areas.admin.ViewModels.Product
{
    public class ProductCreateVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }
    }
}
