using System.Collections.Generic;

namespace CardIndex.Models
{
    public class Genre
    {
        public long GenreId { get; set; }
        public string Name { get; set; }

        public List<BookGenre> Books { get; set; }
    }
}
