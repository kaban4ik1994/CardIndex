using CardIndex.Data.DBInteractions.Concrete;
using CardIndex.Data.DBInteractions.Interface;
using CardIndex.Data.Repositories.Interface;
using CardIndex.Entities;

namespace CardIndex.Data.Repositories.Concrete
{
    public class AuthorRepository : EntityRepositoryBase<DbAuthor>, IAuthorRepository
    {
        public AuthorRepository(IDbFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
