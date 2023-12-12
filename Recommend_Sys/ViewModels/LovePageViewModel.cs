using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Recommend_Sys.Models;
using Recommend_Sys.Repositories;

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
        public UserModel? CurrentUser
        {
            get
            {
               
                return _mainWindowViewModel.CurrentUser;
            }
        }

        [ObservableProperty]
        private ObservableCollection<SongModel> _lovesongs;



        public LovePageViewModel()
        {
            userRepository = new UserRepository();

        }
        [RelayCommand]
        public void LoadLoveSongs()
        {
            MessageBox.Show("LoadLoveSongs");
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            LoveSongRepository loveSongRepository = new LoveSongRepository();
            var lovesongs = loveSongRepository.GetLoveSongs(user.Id);
            Lovesongs = new ObservableCollection<SongModel>(lovesongs);
            


        }

        public LovePageViewModel(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            //LoadLoveSongs();
        }
        [RelayCommand]
        private void ChangeSongSource(string songSource)
        {
            SongSource = songSource;
        }

        [RelayCommand]
        private void PlaySong(SongModel song)
        {
            if (song.url != null)
            {
                ChangeSongSource(song.url);
            }
        }
    }
}
