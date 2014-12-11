using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardIndex.Entities
{
    [Table("BookAuthors")]
    public class DbBookDbAuthor
    {

        [Key, Column(Order = 0)]
        public long BookId { get; set; }
        [Key, Column(Order = 1)]
        public long AuthorId { get; set; }

        public virtual DbBook Book { get; set; }
        public virtual DbAuthor Author { get; set; }
    }
}
