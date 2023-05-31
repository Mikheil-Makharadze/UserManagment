using Microsoft.AspNetCore.Identity;

namespace UserManagment.Core.Entities
{
    public class User : IdentityUser
    {
        public bool IsActive { get; set; }

        // Navigation property for UserProfile
        public UserProfile? UserProfile { get; set; }
    }
}
