using Albums.Data;
using System.Threading.Tasks;

namespace Albums
{
    public interface IMusicServer
    {
        /// <summary>
        /// Look up all albums for artist by artist name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<Artist> GetArtistWithAlbumsAsync(string name);
        Task<bool> IsAvaliableAsync();

    }

}
