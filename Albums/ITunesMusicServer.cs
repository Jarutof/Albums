using Albums.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Albums
{
    public class ITunesMusicServer : IMusicServer
    {
        private const string SERVER = "itunes.apple.com";
        private static string Path => $"https://{SERVER}";

        private readonly HttpClient _client = new HttpClient();
        private async Task<IEnumerable<string>> GetAlbumsAsync(int id)
        {
            UriBuilder uriBuilder = new UriBuilder(Path + "/lookup")
            {
                Query = $"id={id}&entity=album"
            };
            var response = await _client.GetAsync(uriBuilder.Uri);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                var data = (JObject)JsonConvert.DeserializeObject(content);
                return data["results"]
                    .Select(field => field["collectionName"])
                    .Where(c => c != null)
                    .ToList()
                    .Select(c => c.ToString());
            }
            return null;
        }

        public async Task<Artist> GetArtistWithAlbumsAsync(string name)
        {
            var artist = await GetArtistAsync(name);
            if (artist == null) return null;

            var albums = await GetAlbumsAsync(artist.ArtistId);
            List<Album> albumsList = albums.Select(a => new Album() { Name = a, Artist = artist }).ToList();

            artist.Albums = albumsList;
            return artist;
        }

        private async Task<Artist> GetArtistAsync(string name)
        {
            UriBuilder uriBuilder = new UriBuilder(Path + "/search")
            {
                Query = $"term={GetQueryParamString(name)}"
            };
            var response = await _client.GetAsync(uriBuilder.Uri);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                var data = (JObject)JsonConvert.DeserializeObject(content);
                if (data["results"].First == null) return null;

                var astistId = data["results"].First["artistId"];
                var artistName = data["results"].First["artistName"];
                if (astistId == null || artistName == null) return null;
                return new Artist
                {
                    ArtistId = astistId.Value<int>(),
                    Name = artistName.Value<string>()
                };
            }
            return null;
        }
        private static string GetQueryParamString(string str) => str.Replace(' ', '+');
        public async Task<bool> IsAvaliableAsync()
        {
            Ping ping = new Ping();
            try
            {
                var res = await ping.SendPingAsync(SERVER);
                return res.Status == IPStatus.Success;
            }
            catch
            {
                return false;
            }
        }
    }
}
