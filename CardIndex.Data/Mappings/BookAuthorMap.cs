using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CardIndex.Entities;

namespace CardIndex.Data.Mappings
{
    public class BookAuthorMap : EntityTypeConfiguration<DbBookDbAuthor>
    {
        public BookAuthorMap()
        {
            //key  
            HasKey(t => t.AuthorId);
            HasKey(t => t.BookId);

            //property
            Property(t => t.AuthorId);
            Property(t => t.BookId);

            //table  
            ToTable("BookAuthor");

            //relationship
            HasRequired(t => t.Author).WithMany(c => c.Books).HasForeignKey(p => p.AuthorId);
          //  HasRequired(t => t.Book).WithMany(c => c.Authors).HasForeignKey(p => p.BookId);

        }
    }
}
