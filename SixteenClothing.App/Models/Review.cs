using SixteenClothing.App.Models.BaseModels;

namespace SixteenClothing.App.Models
{
    public class Review : BaseEntity
    {
        public string Text { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
