using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardIndex.Entities
{
    [Table("Book")]
    public class DbBook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        [Column("ISBN")] 
        public string Isbn { get; set; }
        public string Etc { get; set; }

        public virtual ICollection<DbBookDbGenre> Genres { get; set; }
        public virtual ICollection<DbBookDbAuthor> Authors { get; set; }
    }
}
