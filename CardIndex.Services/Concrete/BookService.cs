using System.Collections.Generic;
using CardIndex.Data.DBInteractions.Interface;
using CardIndex.Data.Repositories.Interface;
using CardIndex.Entities;
using CardIndex.Services.Interface;
using System.Linq;

namespace CardIndex.Services.Concrete
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookAuthorRepository _bookAuthorRepository;
        private readonly IBookGenreRepository _bookGenreRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IBookRepository bookRepository, IBookAuthorRepository bookAuthorRepository,
            IBookGenreRepository bookGenreRepository, IAuthorRepository authorRepository,
            IGenreRepository genreRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _bookAuthorRepository = bookAuthorRepository;
            _bookGenreRepository = bookGenreRepository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
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

        public long GetCount()
        {
            return _bookRepository.GetAll().Count();
        }

        public IEnumerable<DbBook> GetSortedBooks(bool sortDirection, int sortColumn)
        {
            var books = _bookRepository.GetAll();
            switch (sortColumn)
            {
                case 0: books = sortDirection ? books.OrderBy(x => x.Id) : books.OrderByDescending(x => x.Id); break;
                case 1: books = sortDirection ? books.OrderBy(x => x.Name) : books.OrderByDescending(x => x.Name); break;
                case 2: books = sortDirection ? books.OrderBy(x => x.Isbn) : books.OrderByDescending(x => x.Isbn); break;
                case 3: books = sortDirection ? books.OrderBy(x => x.Etc) : books.OrderByDescending(x => x.Etc); break;
                case 4: break; //sort by authors
                case 5: break; //sort by genres
            }
            return books;
        }

        public void CreateBook(DbBook book)
        {
            _bookRepository.Add(book);
            _unitOfWork.Commit();
        }

        public void UpdateBook(DbBook book)
        {
            
            foreach (var author in book.Authors)
            {
                
            }

            foreach (var genre in book.Genres)
            {
                _bookGenreRepository.Attach(genre);
            }

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
