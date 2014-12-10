﻿using CardIndex.Data.DBInteractions.Interface;
using CardIndex.Entities;

namespace CardIndex.Data.Repositories.Interface
{
    public interface IBookRepository : IEntityRepository<DbBook>
    {
    }
}
