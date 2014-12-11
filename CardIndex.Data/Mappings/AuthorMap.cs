using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CardIndex.Entities;

namespace CardIndex.Data.Mappings
{
    public class AuthorMap : EntityTypeConfiguration<DbAuthor>
    {
        public AuthorMap()
        {
            //key  
            HasKey(t => t.Id);

            //property
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);  
            Property(t => t.Name).IsRequired();

            //table  
            ToTable("Author");

            //relationship  
        }
    }
}
