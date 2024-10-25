using Microsoft.AspNetCore.Identity;

namespace boardroom_management.Data
{
    public class ApplicationUser : IdentityUser
    {
        // Additional properties for your user
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}