using SixteenClothing.App.Enums;
using SixteenClothing.App.Models.BaseModels;

namespace SixteenClothing.App.Models
{
    public class Rating : BaseEntity
    {
        public RatingScore RatingScore { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
    }
}
