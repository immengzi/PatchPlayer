using Newtonsoft.Json.Linq;
using Recommend_Sys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Recommend_Sys.Services
{
    public class UpdateSong
    {
        public static async Task UpdateSongUrlAsync(SongModel songModel)
        {
            int songId = songModel.id;
            string apiEndpoint = Server.ServerUrl;
            string getSongQuery = $"/song/url?id={songId}";
            string getSongUrl = $"{apiEndpoint}{getSongQuery}";
            string getSongResponse = await GetApiData(getSongUrl);
            JObject jsonGetSongResponse = JObject.Parse(getSongResponse);
            JArray songData = (JArray)jsonGetSongResponse["data"];
            var songItem = songData[0];
            string? url = (string?)songItem["url"];
            if (!string.IsNullOrEmpty(url))
            {
                songModel.url = url;
            }
        }

        private static async Task<string> GetApiData(string apiUrl)
        {
            return await GetApi.GetApiData(apiUrl);
        }
    }
}
