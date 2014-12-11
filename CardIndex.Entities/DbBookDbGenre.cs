using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardIndex.Entities
{
    [Table("GenreBooks")]
    public class DbBookDbGenre
    {
        [Key, Column(Order = 0)]
        public long BookId { get; set; }
        [Key, Column(Order = 1)]
        public long GenreId { get; set; }

        public virtual DbBook Book { get; set; }
        public virtual DbGenre Genre { get; set; }
    }
}
