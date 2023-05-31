using System.ComponentModel.DataAnnotations;

namespace UserManagment.Core.Entities
{
    public class UserProfile : BaseEntity
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [StringLength(11)]
        public string PersonalNumber { get; set; }

        // Navigation property for User
        public string UserId { get; set; }
        public User User { get; set; }
    }

}
