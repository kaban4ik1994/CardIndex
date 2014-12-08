using CardIndex.DBInteractions.Concrete;
using CardIndex.DBInteractions.Interface;
using CardIndex.Entities;
using CardIndex.Repositories.Interface;

namespace CardIndex.Repositories.Concrete
{
    public class GenreRepository : EntityRepositoryBase<DbGenre>, IGenreRepository
    {
        public GenreRepository(IDbFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
