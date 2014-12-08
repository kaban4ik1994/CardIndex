using System.Data.Entity;
using CardIndex.Entities;

namespace CardIndex.Context
{
    public class CardIndexContext:DbContext
    {
        public CardIndexContext()
            : base("CardIndexDBConnectionString")
        {

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
