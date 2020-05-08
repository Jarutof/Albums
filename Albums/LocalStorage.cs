using Albums.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Albums
{
    class LocalStorage : IArtistStorage
    {
        public Artist GetArtistByName(string name)
        {
            using ApplicationContext context = new ApplicationContext();
            var artist = context.Artists.Where(a => a.Name.ToLower() == name).Include(c => c.Albums).First();
            return artist;
        }
        public void SaveArtist(Artist artist)
        {
            using ApplicationContext context = new ApplicationContext();

            var existingArtist = context.Artists.Where(a => a.ArtistId == artist.ArtistId).FirstOrDefault();
            if (existingArtist == null)
            {
                context.Artists.Add(artist);
                context.SaveChanges();
            }
        }
    }
}
