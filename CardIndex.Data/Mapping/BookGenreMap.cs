using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CardIndex.Entities;

namespace CardIndex.Data.Mapping
{
    public class BookGenreMap : EntityTypeConfiguration<DbBookDbGenre>
    {
        public BookGenreMap()
        {
            HasKey(t => t.Id);

            //property  
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //table  
            ToTable("GenreBooks");

            //relationship  
            HasRequired(p => p.Genre).WithMany(x => x.Books).HasForeignKey(x => x.GenreId);
            HasRequired(p => p.Book).WithMany(x => x.Genres).HasForeignKey(x => x.BookId);
        }
    }
}
