using System.Collections.Generic;
using CardIndex.Entities;

namespace CardIndex.Services.Interface
{
    public interface IGenreService
    {
        IEnumerable<DbGenre> GetGenres();
        DbGenre GetGenreById(long id);
        void CreateGenre(DbGenre genre);
        void UpdateGenre(DbGenre genre);
        void DeleteGenre(long id);
        void SaveGenre();
    }
}
