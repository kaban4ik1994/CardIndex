using System;

namespace CardIndex.Data.DBInteractions.Interface
{
    public interface IDbFactory : IDisposable
    {
        CardIndexContext Get();
    }
}
