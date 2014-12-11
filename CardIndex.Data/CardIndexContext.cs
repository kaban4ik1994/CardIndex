using System.Data.Entity;
using CardIndex.Entities;

namespace CardIndex.Data
{
    public class CardIndexContext : DbContext
    {
        public CardIndexContext()
            : base("CardIndexDBConnectionString")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbBook>()
                .HasMany(x => x.Authors)
                .WithMany(x => x.Books)
                .Map(configuration =>
                {
                    configuration.ToTable("DbBookDbAuthors");
                    configuration.MapLeftKey("DbBook_Id");
                    configuration.MapRightKey("DbAuthor_Id");
                });

            modelBuilder.Entity<DbBook>()
                .HasMany(x => x.Genres)
                .WithMany(x => x.Books)
                .Map(configuration =>
                {
                    configuration.ToTable("DbGenreDbBooks");
                    configuration.MapLeftKey("DbBook_Id");
                    configuration.MapRightKey("DbGenre_Id");
                });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<DbBook> Books { get; set; }
        public DbSet<DbAuthor> Authors { get; set; }
        public DbSet<DbGenre> Genres { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }
    }
}
