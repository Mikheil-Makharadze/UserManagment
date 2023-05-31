using UserManagment.Core.Entities;

namespace UserManagment.Core.Interfaces
{
    public interface IUserProfileService : IGenericRepository<UserProfile>
    {
        Task<UserProfile> GetUserProfileByUserId(string userId); 
    }
}
