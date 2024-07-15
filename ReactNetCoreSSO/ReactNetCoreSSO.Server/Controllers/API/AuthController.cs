using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Zeller3Dcatalog.Server.Models;
using ReactNetCoreSSO.Server.ModelResources;
using ReactNetCoreSSO.Server.Services;

namespace ReactNetCoreSSO.Server.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginModel request)
        {
            var user = await _userService.GetUserAsync(request);
            if (user == null) return Unauthorized();



            string token = CreateToken(user);

            return Ok(new { accessToken= token, role = user.Role });
        }

        [HttpPost("Register")]
        public async Task<ActionResult<string>> Register(LoginModel request)
        {
            byte[] passwordSalt;
            byte[] passwordHash;

            createPasswordHash(request, out passwordHash, out passwordSalt);

            var user = new User()
            {
                Email = request.Email,
                Role = "User",
                PasswordHash = System.Text.Encoding.UTF8.GetString(passwordHash),
                PasswordSalt = System.Text.Encoding.UTF8.GetString(passwordSalt),
                Name = request.Name
            };

            await _userService.AddUserAsync(user);

            return Created();
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"],
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = creds,
                Subject = new ClaimsIdentity(claims)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private void createPasswordHash(LoginModel loginModel, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(loginModel.Password));
            }
        }

        private bool verifyPasswordHash(LoginModel loginModel, User user)
        {
            using (var hmac = new HMACSHA512(System.Text.Encoding.UTF8.GetBytes(user.PasswordSalt)))
            {
                var computehash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(loginModel.Password));
                return computehash.SequenceEqual(System.Text.Encoding.UTF8.GetBytes(user.PasswordHash));
            }
            return false;
        }
    }
}
