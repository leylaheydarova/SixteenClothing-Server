using System.ComponentModel.DataAnnotations;

namespace SixteenClothing.App.Areas.admin.ViewModels.Category
{
    public class CategoryCreateVM
    {
        [Required]
        public string Name { get; set; }
    }
}
