using CardIndex.DBInteractions.Concrete;
using CardIndex.DBInteractions.Interface;
using CardIndex.Entities;
using CardIndex.Repositories.Interface;

namespace CardIndex.Repositories.Concrete
{
    public class AuthorRepository : EntityRepositoryBase<DbAuthor>, IAuthorRepository
    {
        public AuthorRepository(IDbFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
