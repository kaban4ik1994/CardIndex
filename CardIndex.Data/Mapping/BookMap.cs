using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CardIndex.Entities;

namespace CardIndex.Data.Mapping
{
    public class BookMap : EntityTypeConfiguration<DbBook>
    {
        public BookMap()
        {
            HasKey(t => t.Id);

            //property  
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Name);
            Property(t => t.Isbn);
            Property(t => t.Etc);

            //table  
            ToTable("Book");

            //relationship  
        }
    }
}
