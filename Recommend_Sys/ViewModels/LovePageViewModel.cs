﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Recommend_Sys.Models;
using Recommend_Sys.Repositories;
using Recommend_Sys.Services;

namespace Recommend_Sys.ViewModels
{
    public partial class LovePageViewModel : ObservableObject
    {
        private readonly MainWindowViewModel _mainWindowViewModel;

        public string? SongSource
        {
            get { return _mainWindowViewModel.SongSource; }
            set { _mainWindowViewModel.SongSource = value; }
        }

        private IUserRepository userRepository;
        private LoveSongRepository loveSongRepository;

        [ObservableProperty]
        private ObservableCollection<SongModel> _lovesongs;

        public LovePageViewModel() { }

        public LovePageViewModel(MainWindowViewModel mainWindowViewModel)
        {
            userRepository = new UserRepository();
            loveSongRepository = new LoveSongRepository();
            _lovesongs = new ObservableCollection<SongModel>();
            _mainWindowViewModel = mainWindowViewModel;
        }

        [RelayCommand]
        public async Task LoadLoveSongs()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            var lovesongs = loveSongRepository.GetLoveSongs(user.Id);
            foreach (var lovesong in lovesongs)
            {
                await UpdateSong.UpdateSongUrlAsync(lovesong);
                Lovesongs.Add(new SongModel()
                {
                    name = lovesong.name,
                    artist_name = lovesong.artist_name,
                    album_name = lovesong.album_name,
                    id = lovesong.id,
                    url = lovesong.url
                });
            }
        }

        [RelayCommand]
        private void ChangeSongSource(string songSource)
        {
            SongSource = songSource;
            //MessageBox.Show("当前播放的歌曲URL是" + SongSource);
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

        [RelayCommand]
        private void AddToLove(SongModel songModel)
        {
            if (songModel.url != null)
            {
                LoveSongRepository loveSongRepository = new LoveSongRepository();
                loveSongRepository.AddLoveSong(_mainWindowViewModel.CurrentUser.Id, songModel);
                MessageBox.Show("添加成功");
            }
            else
            {
                return;
            }
        }

        [RelayCommand]
        private void RemoveFromLove(SongModel songModel)
        {
            if (songModel.url != null)
            {
                LoveSongRepository loveSongRepository = new LoveSongRepository();
                loveSongRepository.DeleteLoveSong(_mainWindowViewModel.CurrentUser.Id, songModel);
                MessageBox.Show("删除成功");
            }
            else
            {
                return;
            }
        }
    }
}
