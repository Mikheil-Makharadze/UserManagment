using UserManagment.Core.Entities;

namespace UserManagment.Core.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}
