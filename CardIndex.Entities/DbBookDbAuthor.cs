using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardIndex.Entities
{
    public class DbBookDbAuthor
    {
        public long Id { get; set; }
        public long BookId { get; set; }
        public long AuthorId { get; set; }

        public virtual DbBook Book { get; set; }
        public virtual DbAuthor Author { get; set; }
    }
}
