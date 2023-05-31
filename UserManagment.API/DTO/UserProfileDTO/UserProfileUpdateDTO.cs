using System.ComponentModel.DataAnnotations;

namespace UserManagment.API.DTO.UserProfileDTO
{
    public class UserProfileUpdateDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [StringLength(11)]
        public string PersonalNumber { get; set; }
    }
}
