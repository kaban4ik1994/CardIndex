using CardIndex.Data.DBInteractions.Concrete;
using CardIndex.Data.DBInteractions.Interface;
using CardIndex.Data.Repositories.Interface;
using CardIndex.Entities;

namespace CardIndex.Data.Repositories.Concrete
{
    public class BookRepository : EntityRepositoryBase<DbBook>, IBookRepository
    {
        public BookRepository(IDbFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
