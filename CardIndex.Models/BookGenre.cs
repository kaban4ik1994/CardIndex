namespace CardIndex.Models
{
    public class BookGenre
    {
        public long BookId { get; set; }
        public long GenreId { get; set; }

        public Book Book { get; set; }
        public Genre Genre { get; set; }
    }
}
