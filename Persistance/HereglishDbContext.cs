using Hereglish.Models;
using Microsoft.EntityFrameworkCore;

namespace Hereglish.Persistance
{
    public class HereglishDbContext : DbContext
    {
        public HereglishDbContext(DbContextOptions<HereglishDbContext> options)
            : base(options)
        {

        }

        public DbSet <Category> Categories{ get; set; }

    }
}