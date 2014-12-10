using CardIndex.Data.DBInteractions.Concrete;
using CardIndex.Data.DBInteractions.Interface;
using CardIndex.Data.Repositories.Interface;
using CardIndex.Entities;

namespace CardIndex.Data.Repositories.Concrete
{
    public class GenreRepository : EntityRepositoryBase<DbGenre>, IGenreRepository
    {
        public GenreRepository(IDbFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
