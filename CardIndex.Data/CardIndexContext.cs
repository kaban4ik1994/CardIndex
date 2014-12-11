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
