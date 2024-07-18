using Microsoft.EntityFrameworkCore;

namespace EFCoreAdvanced.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
