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
            List<SongModel> songs = new List<SongModel>();
            //云服务器
            string apiEndpoint = "http://api.immengzi.wiki:3000";
            //本地服务器
            //string apiEndpoint = "http://localhost:3000";
            string searchQuery = $"/cloudsearch?keywords={songName}";
            string apiUrl = $"{apiEndpoint}{searchQuery}";
            string searchResponse = await GetApiData(apiUrl);

            JObject jsonSearchResponse = JObject.Parse(searchResponse);

            try
            {
                //MessageBox.Show(jsonSearchResponse.ToString());
                JArray searchSongs = (JArray)jsonSearchResponse["result"]["songs"];
                foreach (var searchSong in searchSongs)
                {
                    int? songId = (int?)searchSong["id"];
                    string? realSongName = (string?)searchSong["name"];
                    string? artistName = (string?)searchSong["ar"]?[0]["name"];
                    string? albumName = (string?)searchSong["al"]?["name"];

                    string getSongQuery = $"/song/url?id={songId}";
                    string getSongUrl = $"{apiEndpoint}{getSongQuery}";
                    string getSongResponse = await GetApiData(getSongUrl);

                    if (string.IsNullOrEmpty(getSongResponse))
                    {
                        break;
                    }

                    JObject jsonGetSongResponse = JObject.Parse(getSongResponse);
                    JArray songData = (JArray)jsonGetSongResponse["data"];

                    foreach (var item in songData)
                    {
                        string? url = (string?)item["url"];
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
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return songs;
        }

        static async Task<string> GetApiData(string apiUrl)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage httpResponse = await httpClient.GetAsync(apiUrl);

                    if (httpResponse.IsSuccessStatusCode)
                    {
                        return await httpResponse.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        Console.WriteLine($"HTTP请求失败: {httpResponse.StatusCode}");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"发生异常: {ex.Message}");
                    return null;
                }
            }
        }
    }
}
