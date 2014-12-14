using System.Collections.Generic;
using System.Linq;
using CardIndex.Data.DBInteractions.Interface;
using CardIndex.Data.Repositories.Interface;
using CardIndex.Entities;
using CardIndex.Services.Interface;

namespace CardIndex.Services.Concrete
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookAuthorRepository _bookAuthorRepository;
        private readonly IBookGenreRepository _bookGenreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IBookRepository bookRepository, IBookAuthorRepository bookAuthorRepository, IBookGenreRepository bookGenreRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _bookAuthorRepository = bookAuthorRepository;
            _bookGenreRepository = bookGenreRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<DbBook> GetBooks()
        {
            var books = _bookRepository.GetAll();
            return books;
        }

        public DbBook GetBookById(long id)
        {
            var book = _bookRepository.GetById(id);
            return book;
        }

        public void CreateBook(DbBook book)
        {
            _bookRepository.Add(book);
            _unitOfWork.Commit();
        }

        public void UpdateBook(DbBook book)
        {
            //hardcode
            var authors = new List<DbBookDbAuthor>(book.Authors);
            var genres = new List<DbBookDbGenre>(book.Genres);
            authors.ForEach(x => x.BookId = book.Id);
            genres.ForEach(x => x.BookId = book.Id);

            var originalAuthors = new List<DbBookDbAuthor>(_bookAuthorRepository.GetAll().Where(x => x.BookId == book.Id));
            var originalGenres = new List<DbBookDbGenre>(_bookGenreRepository.GetAll().Where(x => x.BookId == book.Id));

            originalAuthors.ForEach(x => _bookAuthorRepository.Delete(x));
            originalGenres.ForEach(x => _bookGenreRepository.Delete(x));
            _unitOfWork.Commit();

            authors.ForEach(x => _bookAuthorRepository.Add(x));
            genres.ForEach(x => _bookGenreRepository.Add(x));
            _unitOfWork.Commit();

            book.Authors.Clear();
            book.Genres.Clear();
            _bookRepository.Update(book);
            _unitOfWork.Commit();

        }

        public void DeleteBook(long id)
        {
            var genre = _bookRepository.GetById(id);
            _bookRepository.Delete(genre);
            _unitOfWork.Commit();
        }

        public void SaveBook()
        {
            _unitOfWork.Commit();
        }
    }
}
