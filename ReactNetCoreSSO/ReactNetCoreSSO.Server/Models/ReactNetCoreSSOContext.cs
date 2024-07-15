using Microsoft.EntityFrameworkCore;

namespace Zeller3Dcatalog.Server.Models
{
    public class ReactNetCoreSSOContext : DbContext
    {
        public ReactNetCoreSSOContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
