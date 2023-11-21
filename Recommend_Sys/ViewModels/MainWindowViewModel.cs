using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using FlyleafLib;
using FlyleafLib.MediaPlayer;

namespace Recommend_Sys.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private MediaPlayer _player;

        public MainWindowViewModel() { }
    }
}
