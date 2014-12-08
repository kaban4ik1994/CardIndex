using System.Data.Entity;

namespace CardIndex.Context
{
    public class CardIndexContextInitializer: DropCreateDatabaseIfModelChanges<CardIndexContext>
    {
        protected override void Seed(CardIndexContext context)
        {
            base.Seed(context);
        }
    }
}
