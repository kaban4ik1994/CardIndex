using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardIndex.Entities
{
    [Table("GenreBooks")]
    public class DbBookDbGenre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [ForeignKey("Book")]
        public long BookId { get; set; }
        [ForeignKey("Genre")]
        public long GenreId { get; set; }

        public virtual DbBook Book { get; set; }
        public virtual DbGenre Genre { get; set; }
    }
}
