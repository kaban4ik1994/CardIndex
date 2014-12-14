using System.Collections.Generic;

namespace CardIndex.Entities
{
    public class DbGenre
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<DbBookDbGenre> Books { get; set; }
 
    }
}
