using UserManagment.infrastructure.Data.DB;
using UserManagment.Core.Entities;
using UserManagment.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace UserManagment.infrastructure.Services
{
    public class UserProfileService :GenericRepository<UserProfile>, IUserProfileService
    {
        private readonly AppDbContext Context;

        public UserProfileService(AppDbContext context) : base(context)
        {
            Context = context;
        }

        public async Task<UserProfile> GetUserProfileByUserId(string userId)
        {
            return await Context.UserProfiles.Where(n => n.UserId == userId).FirstOrDefaultAsync();
        }
    }
}
