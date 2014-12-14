using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardIndex.Entities
{
    [Table("Author")]
    public class DbAuthor
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<DbBookDbAuthor> Books { get; set; }
    }
}
