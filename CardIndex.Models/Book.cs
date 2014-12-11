using System.Collections.Generic;

namespace CardIndex.Models
{
    public class Book
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Isbn { get; set; }
        public string Etc { get; set; }

        public List<Genre> Genres { get; set; }
        public List<Author> Authors { get; set; }
    }
}
