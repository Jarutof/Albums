using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Albums.Data
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Album> Albums { get; set; }
        public int ArtistId { get; set; }
    }
}
