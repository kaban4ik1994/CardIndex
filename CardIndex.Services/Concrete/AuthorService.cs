using System.Collections.Generic;
using System.Linq;
using CardIndex.Data.DBInteractions.Interface;
using CardIndex.Data.Repositories.Interface;
using CardIndex.Entities;
using CardIndex.Services.Interface;

namespace CardIndex.Services.Concrete
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AuthorService(IAuthorRepository authorRepository, IUnitOfWork unitOfWork)
        {
            _authorRepository = authorRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<DbAuthor> GetAuthors()
        {
            var authors = _authorRepository.GetAll();
            return authors;
        }

        public DbAuthor GetAuthorById(long id)
        {
            var author = _authorRepository.GetById(id);
            return author;
        }

        public long GetCount()
        {
            return _authorRepository.GetAll().Count();
        }

        public void CreateAuthor(DbAuthor author)
        {
            _authorRepository.Add(author);
            _unitOfWork.Commit();
        }

        public void UpdateAuthor(DbAuthor author)
        {
            _authorRepository.Update(author);
            _unitOfWork.Commit();
        }

        public void DeleteAuthor(long id)
        {
            var author = _authorRepository.GetById(id);
            _authorRepository.Delete(author);
            _unitOfWork.Commit();
        }

        public void SaveAuthor()
        {
            _unitOfWork.Commit();
        }
    }
}
