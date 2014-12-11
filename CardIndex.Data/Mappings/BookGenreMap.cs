using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using CardIndex.Entities;

namespace CardIndex.Data.Mappings
{
    public class BookGenreMap : EntityTypeConfiguration<DbBookDbGenre>
    {
        public BookGenreMap()
        {
            //key  
            HasKey(t => t.GenreId);
            HasKey(t => t.BookId);

            //property
            Property(t => t.GenreId);
            Property(t => t.BookId);

            //table  
            ToTable("BookGenre");

            //relationship
            HasRequired(t => t.Genre).WithMany(c => c.Books).HasForeignKey(p => p.GenreId);
          //  HasRequired(t => t.Book).WithMany(c => c.Genres).HasForeignKey(p => p.BookId);
        }
    }
}
