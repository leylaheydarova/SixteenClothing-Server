using SixteenClothing.App.Models.BaseModels;

namespace SixteenClothing.App.Models
{
    public class Slider : BaseEntity
    {
        public string Heading { get; set; }
        public string Text { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
    }
}
