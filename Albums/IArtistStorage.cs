using Albums.Data;

namespace Albums
{
    interface IArtistStorage
    {
        Artist GetArtistByName(string artistName);
        void SaveArtist(Artist artist);
    }
}
