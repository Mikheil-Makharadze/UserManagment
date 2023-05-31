namespace UserManagment.API.DTO.UserProfileDTO
{
    public class UserProfileDetailDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PersonalNumber { get; set; }


        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        
    }
}
