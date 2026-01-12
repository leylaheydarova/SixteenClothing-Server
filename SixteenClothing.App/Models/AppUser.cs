using Microsoft.AspNetCore.Identity;

namespace SixteenClothing.App.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
