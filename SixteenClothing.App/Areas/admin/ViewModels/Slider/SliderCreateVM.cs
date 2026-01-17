using System.ComponentModel.DataAnnotations;

namespace SixteenClothing.App.Areas.admin.ViewModels.Slider
{
    public class SliderCreateVM
    {
        [Required]
        [MaxLength(100)]
        public string Heading { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Text { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
