using CardIndex.Data.DBInteractions.Interface;

namespace CardIndex.Data.DBInteractions.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory _databaseFactory;
        private CardIndexContext _dataContext;

        public UnitOfWork(IDbFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        protected CardIndexContext DataContext
        {
            get { return _dataContext ?? (_dataContext = _databaseFactory.Get()); }
        }

        public void Commit()
        {
            DataContext.Commit();
        }
    }
}
