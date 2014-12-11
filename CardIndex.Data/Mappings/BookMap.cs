using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CardIndex.Entities;

namespace CardIndex.Data.Mappings
{
    class BookMap : EntityTypeConfiguration<DbBook>
    {
        public BookMap()
        {
            //key  
            HasKey(t => t.Id);

            //property   
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Name).IsRequired();
            Property(t => t.Isbn).IsRequired();
            Property(t => t.Etc).IsRequired();
            //table  
            ToTable("Book");

            //relationship  
        }
    }
}
