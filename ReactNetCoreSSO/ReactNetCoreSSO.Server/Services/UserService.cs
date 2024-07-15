using Microsoft.EntityFrameworkCore;
using Zeller3Dcatalog.Server.Models;
using ReactNetCoreSSO.Server.ModelResources;

namespace ReactNetCoreSSO.Server.Services
{
    public class UserService : IUserService
    {
        private readonly ReactNetCoreSSOContext _context;
        public UserService(ReactNetCoreSSOContext context)
        {
            _context = context;
        }
        public async Task<User?> GetUserAsync(LoginModel req)
        {
            var user = await _context.Users.Where(u => u.Email == req.Email).FirstOrDefaultAsync();
            if (user == null)
            {
                return null;
            }
            return user;
        }
        public async Task<User?> AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
