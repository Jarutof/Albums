using Albums.Data;
using System;
using System.Threading.Tasks;

namespace Albums
{

    class Program
    {
        static async Task Main()
        {
            IMusicServer _musicServer = new ITunesMusicServer();
            IArtistStorage _storage = new LocalStorage();

            Artist artist;
            while (true)
            {
                Console.Write("Input artist name: ");
                var artistName = Console.ReadLine().RemoveExtraWhiteSpace().ToLower();

                if (await _musicServer.IsAvaliableAsync())
                {
                    artist = await _musicServer.GetArtistWithAlbumsAsync(artistName);
                    if (artist != null)
                    {
                        _storage.SaveArtist(artist);
                        ShowArtist(artist);
                        continue;
                    }
                }


                artist = _storage.GetArtistByName(artistName);

                if (artist != null)
                {
                    ShowArtist(artist);
                }
                else
                {
                    Console.WriteLine($"Albums of \"{artistName}\" not found");
                }
            }
        }

        private static void ShowArtist(Artist artist)
        {
            foreach (var album in artist.Albums)
            {
                Console.WriteLine($"{artist.Name} - {album.Name}");
            }
        }
    }
}
