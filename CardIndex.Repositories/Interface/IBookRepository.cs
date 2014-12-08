using CardIndex.DBInteractions.Interface;
using CardIndex.Entities;

namespace CardIndex.Repositories.Interface
{
    public interface IBookRepository : IEntityRepository<DbBook>
    {
    }
}
