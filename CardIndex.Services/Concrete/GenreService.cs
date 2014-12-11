using System.Collections.Generic;
using CardIndex.Data.DBInteractions.Interface;
using CardIndex.Data.Repositories.Interface;
using CardIndex.Entities;
using CardIndex.Services.Interface;
using System.Linq;

namespace CardIndex.Services.Concrete
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GenreService(IGenreRepository genreRepository, IUnitOfWork unitOfWork)
        {
            _genreRepository = genreRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<DbGenre> GetGenres()
        {
            var genres = _genreRepository.GetAll();
            return genres;
        }

        public DbGenre GetGenreById(long id)
        {
            var genre = _genreRepository.GetById(id);
            return genre;
        }

        public long GetCount()
        {
            return _genreRepository.GetAll().Count();
        }

        public void CreateGenre(DbGenre genre)
        {
            _genreRepository.Add(genre);
            _unitOfWork.Commit();
        }

        public void UpdateGenre(DbGenre genre)
        {
            _genreRepository.Update(genre);
            _unitOfWork.Commit();
        }

        public void DeleteGenre(long id)
        {
            var genre = _genreRepository.GetById(id);
            _genreRepository.Delete(genre);
            _unitOfWork.Commit();
        }

        public void SaveGenre()
        {
            _unitOfWork.Commit();
        }
    }
}
