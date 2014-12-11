using System.Collections.Generic;

namespace CardIndex.Models
{
    public class Book
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Isbn { get; set; }
        public string Etc { get; set; }

        public List<BookGenre> Genres { get; set; }
        public List<BookAuthor> Authors { get; set; }
    }
}
