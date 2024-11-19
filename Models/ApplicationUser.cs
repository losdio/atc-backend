using Microsoft.AspNetCore.Identity;

namespace atc_backend.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; } = "Trainee";
    }
}
