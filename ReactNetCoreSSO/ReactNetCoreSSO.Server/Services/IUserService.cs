using Zeller3Dcatalog.Server.Models;
using ReactNetCoreSSO.Server.ModelResources;

namespace ReactNetCoreSSO.Server.Services
{
    public interface IUserService
    {
        public Task<User?> GetUserAsync(LoginModel req);
        public Task<User?> AddUserAsync(User user);
    }
}
