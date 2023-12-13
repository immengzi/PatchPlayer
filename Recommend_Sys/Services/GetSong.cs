using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HandyControl.Controls;
using Newtonsoft.Json.Linq;
using Recommend_Sys.Models;

namespace Recommend_Sys.Services
{
    public class GetSong
    {
        public static async Task<List<SongModel>> GetSongAsync(string songName)
        {
            string apiEndpoint = Server.ServerUrl;
            string searchQuery = $"/cloudsearch?keywords={songName}";
            string apiUrl = $"{apiEndpoint}{searchQuery}";
            string searchResponse = await GetApiData(apiUrl);

            JObject jsonSearchResponse = JObject.Parse(searchResponse);

            try
            {
                List<SongModel> songs = ParseSearchResponse(jsonSearchResponse, apiEndpoint);
                return songs;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }

        private static List<SongModel> ParseSearchResponse(JObject jsonSearchResponse, string apiEndpoint)
        {
            List<SongModel> songs = new List<SongModel>();

            JArray searchSongs = (JArray)jsonSearchResponse["result"]["songs"];
            foreach (var searchSong in searchSongs)
            {
                int? songId = (int?)searchSong["id"];
                string? realSongName = (string?)searchSong["name"];
                string? artistName = (string?)searchSong["ar"]?[0]["name"];
                string? albumName = (string?)searchSong["al"]?["name"];

                string getSongQuery = $"/song/url?id={songId}";
                string getSongUrl = $"{apiEndpoint}{getSongQuery}";
                string getSongResponse = GetApiData(getSongUrl).GetAwaiter().GetResult();

                if (string.IsNullOrEmpty(getSongResponse))
                {
                    break;
                }

                JObject jsonGetSongResponse = JObject.Parse(getSongResponse);
                JArray songData = (JArray)jsonGetSongResponse["data"];

                AddSongsFromData(songs, songData, realSongName, songId, artistName, albumName);
            }

            return songs;
        }

        private static void AddSongsFromData(List<SongModel> songs, JArray songData, string? realSongName, int? songId, string? artistName, string? albumName)
        {
            var songItem = songData[0];
            string? url = (string?)songItem["url"];
            if (!string.IsNullOrEmpty(url))
            {
                SongModel s = new SongModel()
                {
                    name = realSongName,
                    id = (int)songId,
                    artist_name = artistName,
                    album_name = albumName,
                    url = url
                };
                songs.Add(s);
            }
        }

        private static async Task<string> GetApiData(string apiUrl)
        {
            return await GetApi.GetApiData(apiUrl);
        }
    }
}
