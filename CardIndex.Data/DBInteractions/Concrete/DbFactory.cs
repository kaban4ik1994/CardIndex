using CardIndex.Data.DBInteractions.Interface;

namespace CardIndex.Data.DBInteractions.Concrete
{
    public class DbFactory : Disposable, IDbFactory
    {
        private CardIndexContext _cardIndexContext;

        public CardIndexContext Get()
        {
            return _cardIndexContext ?? (_cardIndexContext = new CardIndexContext());
        }

        protected override void DisposeCore()
        {
            if (_cardIndexContext != null)
                _cardIndexContext.Dispose();
        }
    }
}
