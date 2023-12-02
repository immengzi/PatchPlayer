using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Recommend_Sys.Models;
using Recommend_Sys.Repositories;
using Recommend_Sys.Views;

namespace Recommend_Sys.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private IUserRepository userRepository;

        [ObservableProperty]
        private object? _currentPage;

        [ObservableProperty] private string? _homeIcon;
        [ObservableProperty] private string? _loveIcon;
        [ObservableProperty] private string? _playgroundIcon;
        [ObservableProperty] private string? _userIcon;

        public MainWindowViewModel()
        {
            setDefaultIcon();
            Navigate("Home");
            userRepository = new UserRepository();
        }

        [RelayCommand]
        public void Navigate(string pageName)
        {
            switch (pageName)
            {
                case "Home":
                    CurrentPage = new HomePage();
                    setDefaultIcon();
                    HomeIcon = "\ue867";
                    break;
                case "Love":
                    CurrentPage = new LovePage();
                    setDefaultIcon();
                    LoveIcon = "\ue849";
                    break;
                case "Playground":
                    CurrentPage = new PlaygroundPage();
                    setDefaultIcon();
                    PlaygroundIcon = "\ue866";
                    break;
                case "User":
                    CurrentPage = new UserPage();
                    setDefaultIcon();
                    UserIcon = "\ue860";
                    break;
                default:
                    throw new ArgumentException("Invalid page name", nameof(pageName));
            }
        }

        public void setDefaultIcon()
        {
            HomeIcon = "\ue7c6";
            LoveIcon = "\ue7df";
            PlaygroundIcon = "\ue7c3";
            UserIcon = "\ue7de";
        }
    }
}
