using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using Recommend_Sys.Models;
using Recommend_Sys.Services;
using MessageBox = HandyControl.Controls.MessageBox;

namespace Recommend_Sys.ViewModels
{
    public partial class HomePageViewModel : ObservableObject
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        
        public string? SongSource
        {
            get { return _mainWindowViewModel.SongSource; }
            set { _mainWindowViewModel.SongSource = value; }
        }

        [ObservableProperty]
        private ObservableCollection<SongModel> _songs;

        [ObservableProperty]
        private string? _text;

        [ObservableProperty]
        private string? _searchIcon;

        [RelayCommand]
        public async Task SearchSongAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(_text))
                {
                    return;
                }
                var searchSongs = await Task.Run(() => GetSong.GetSongAsync(songName: _text));

                // 在 UI 线程上更新属性
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Songs.Clear();

                    foreach (var searchSong in searchSongs)
                    {
                        Songs.Add(new SongModel()
                        {
                            name = searchSong.name,
                            artist_name = searchSong.artist_name,
                            album_name = searchSong.album_name,
                            id = searchSong.id,
                            url = searchSong.url
                        });
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public HomePageViewModel()
        {
        }

        public HomePageViewModel(MainWindowViewModel mainWindowViewModel)
        {
            SearchIcon = "\ue8ef";
            _songs = new ObservableCollection<SongModel>();
            _mainWindowViewModel = mainWindowViewModel;
        }

        [RelayCommand]
        private void ChangeSongSource(string songSource)
        {
            SongSource = songSource;
        }

        [RelayCommand]
        private void PlaySong(SongModel songModel)
        {
            if (songModel.url != null)
            {
                ChangeSongSource(songModel.url);
            }
            else
            {
                return;
            }
        }

    }
}
