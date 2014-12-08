using CardIndex.DBInteractions.Concrete;
using CardIndex.DBInteractions.Interface;
using CardIndex.Entities;
using CardIndex.Repositories.Interface;

namespace CardIndex.Repositories.Concrete
{
    public class BookRepository : EntityRepositoryBase<DbBook>, IBookRepository
    {
        public BookRepository(IDbFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
