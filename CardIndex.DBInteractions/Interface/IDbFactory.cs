using System;
using CardIndex.Context;

namespace CardIndex.DBInteractions.Interface
{
    public interface IDbFactory : IDisposable
    {
        CardIndexContext Get();
    }
}
