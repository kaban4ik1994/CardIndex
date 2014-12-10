using System.Data.Entity;
using CardIndex.Entities;

namespace CardIndex.Data
{
    public class CardIndexContext : DbContext
    {
        public CardIndexContext()
            : base("CardIndexDBConnectionString")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
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
