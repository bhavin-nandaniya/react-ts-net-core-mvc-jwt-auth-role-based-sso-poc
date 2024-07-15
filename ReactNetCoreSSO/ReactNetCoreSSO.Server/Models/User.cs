using System.ComponentModel.DataAnnotations;

namespace Zeller3Dcatalog.Server.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Role { get; set; }

    }
}
