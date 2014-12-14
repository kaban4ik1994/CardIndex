using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CardIndex.Entities;

namespace CardIndex.Data.Mapping
{
    class BookAuthorMap:EntityTypeConfiguration<DbBookDbAuthor>
    {
        public BookAuthorMap()
        {
            HasKey(t => t.Id);

            //property  
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //table  
            ToTable("BookAuthors");

            //relationship  
            HasRequired(p => p.Author).WithMany(x => x.Books).HasForeignKey(x => x.AuthorId);
            HasRequired(p => p.Book).WithMany(x => x.Authors).HasForeignKey(x => x.BookId);
        }
    }
}
