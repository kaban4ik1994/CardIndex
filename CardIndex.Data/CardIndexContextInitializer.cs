using System.Data.Entity;

namespace CardIndex.Data
{
    public class CardIndexContextInitializer: DropCreateDatabaseIfModelChanges<CardIndexContext>
    {
        protected override void Seed(CardIndexContext context)
        {
            base.Seed(context);
        }
    }
}
