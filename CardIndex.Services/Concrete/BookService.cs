using System.Collections.Generic;
using CardIndex.Data.DBInteractions.Interface;
using CardIndex.Data.Repositories.Interface;
using CardIndex.Entities;
using CardIndex.Services.Interface;

namespace CardIndex.Services.Concrete
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
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
            var dbBook = _bookRepository.GetById(book.Id);
            _bookRepository.Delete(dbBook);
            _unitOfWork.Commit();
            _bookRepository.Add(book);
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
