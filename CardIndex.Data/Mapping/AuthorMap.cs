﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CardIndex.Entities;

namespace CardIndex.Data.Mapping
{
    public class AuthorMap : EntityTypeConfiguration<DbAuthor>
    {
        public AuthorMap()
        {
            HasKey(t => t.Id);

            //property  
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Name);

            //table  
            ToTable("Author");

            //relationship  
        }
    }
}
