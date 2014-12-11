using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardIndex.Entities
{
    [Table("BookAuthors")]
    public class DbBookDbAuthor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [ForeignKey("Book")]
        public long BookId { get; set; }
        [ForeignKey("Author")]
        public long AuthorId { get; set; }

        public virtual DbBook Book { get; set; }
        public virtual DbAuthor Author { get; set; }
    }
}
