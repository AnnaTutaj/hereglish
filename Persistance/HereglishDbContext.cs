using Hereglish.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Hereglish.Persistance
{
    public class HereglishDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<PartOfSpeech> PartsOfSpeech { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<Photo> Photos { get; set; }


        public HereglishDbContext(DbContextOptions<HereglishDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WordFeature>().HasKey(wf =>
            new { wf.WordId, wf.FeatureId });
        }


    }
}