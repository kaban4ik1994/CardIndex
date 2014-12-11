using System.Collections.Generic;

namespace CardIndex.Models
{
    public class Author
    {
        public long AuthorId { get; set; }
        public string Name { get; set; }

        public List<BookAuthor> Books { get; set; }
    }
}
