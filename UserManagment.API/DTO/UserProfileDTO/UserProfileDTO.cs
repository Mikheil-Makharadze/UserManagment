using System.ComponentModel.DataAnnotations;

namespace UserManagment.API.DTO.UserProfileDTO
{
    public class UserProfileDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PersonalNumber { get; set; }

    }
}
