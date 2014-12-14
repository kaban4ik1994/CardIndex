using System.Data.Entity;
using CardIndex.Data.Mapping;
using CardIndex.Entities;

namespace CardIndex.Data
{
    public class CardIndexContext : DbContext
    {
        public CardIndexContext()
            : base("CardIndexDBConnectionString")
        {
        //    Configuration.LazyLoadingEnabled = false;
         //   Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AuthorMap());
            modelBuilder.Configurations.Add(new BookAuthorMap());
            modelBuilder.Configurations.Add(new BookGenreMap());
            modelBuilder.Configurations.Add(new BookMap());
            modelBuilder.Configurations.Add(new GenreMap());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<DbBook> Books { get; set; }
        public DbSet<DbAuthor> Authors { get; set; }
        public DbSet<DbGenre> Genres { get; set; }
        public DbSet<DbBookDbAuthor> BookAuthors { get; set; }
        public DbSet<DbBookDbGenre> BookGenres { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }
    }
}
