using System.Collections.Generic;
using Newtonsoft.Json;

namespace CardIndex.Models
{
    public class Genre
    {
        [JsonProperty("Id")]
        public long Id { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Books")]
        public ICollection<Book> Books { get; set; }
    }
}
