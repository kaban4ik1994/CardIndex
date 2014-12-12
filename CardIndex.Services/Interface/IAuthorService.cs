using System.Collections.Generic;
using CardIndex.Entities;
using CardIndex.Models;

namespace CardIndex.Services.Interface
{
    public interface IAuthorService
    {
        IEnumerable<DbAuthor> GetAuthors();
        DbAuthor GetAuthorById(long id);
        long GetCount();
        void CreateAuthor(DbAuthor author);
        void UpdateAuthor(DbAuthor author);
        void DeleteAuthor(long id);
        void SaveAuthor();
    }
}
