using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardIndex.Entities
{
    public class DbBookDbGenre
    {
        public long Id { get; set; }
        public long BookId { get; set; }
        public long GenreId { get; set; }

        public virtual DbBook Book { get; set; }
        public virtual DbGenre Genre { get; set; }
    }
}
