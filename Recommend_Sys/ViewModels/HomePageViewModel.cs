using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using Recommend_Sys.Models;
using Recommend_Sys.Services;

namespace Recommend_Sys.ViewModels
{
    public partial class HomePageViewModel : ObservableObject
    {
        [ObservableProperty]
        public List<Song> _songs;

        [ObservableProperty]
        private string? _text;

        [ObservableProperty]
        private string? _name;

        [ObservableProperty]
        private string? _singer;

        [ObservableProperty]
        private string? _album;

        [RelayCommand]
        public async void SearchSong()
        {
            // 使用Task.Run在后台线程执行GetSongAsync
            var songs = await Task.Run(() => GetSong.GetSongAsync(songName: _text));

            // 在UI线程上更新属性
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (var song in songs)
                {
                    Name = song.name;
                    Singer = song.artist_name;
                    Album = song.album_name;
                }
            });
        }

        public HomePageViewModel()
        {

        }
    }
}
