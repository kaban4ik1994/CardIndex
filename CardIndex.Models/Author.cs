using System.Collections.Generic;

namespace CardIndex.Models
{
    public class Author
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
