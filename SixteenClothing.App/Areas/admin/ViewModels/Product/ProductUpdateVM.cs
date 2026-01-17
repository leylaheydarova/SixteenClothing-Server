namespace SixteenClothing.App.Areas.admin.ViewModels.Product
{
    public class ProductUpdateVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
    }
}
