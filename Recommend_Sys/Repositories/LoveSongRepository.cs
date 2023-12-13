using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Recommend_Sys.Models;


namespace Recommend_Sys.Repositories
{
    public class LoveSongRepository: RepositoryBase
    {
        public List<SongModel> GetLoveSongs(string userId)
        {
            List<SongModel> loveSongs = new List<SongModel>();
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM [LoveSongs] WHERE UserId = @userId", connection);
                command.Parameters.AddWithValue("@userId", userId);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SongModel song = new SongModel();
                        song.id = reader.GetInt32(1);
                        song.name = reader[3].ToString();
                        song.artist_name = reader[4].ToString();
                        song.album_name = reader[5].ToString();
                        song.url = reader[2].ToString();
                        loveSongs.Add(song);
                    }
                }
            }
            return loveSongs;
        }

        public void AddLoveSong(string userId, SongModel song)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO LoveSongs VALUES (@userId, @song_id, @url, @song_name, @artist_name, @album_name,@label)", connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@song_id", song.id);
                command.Parameters.AddWithValue("@song_name", song.name);
                command.Parameters.AddWithValue("@artist_name", song.artist_name);
                command.Parameters.AddWithValue("@album_name", song.album_name);
                command.Parameters.AddWithValue("@url", song.url);
                command.Parameters.AddWithValue("@label", "tmp");
                command.ExecuteNonQuery();
            }
        }

        public void DeleteLoveSong(string userId, SongModel song)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM LoveSongs WHERE UserId = @userId AND SongId = @song_id", connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@song_id", song.id);
                command.ExecuteNonQuery();
            }
        }
    }   
    
}
