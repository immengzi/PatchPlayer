using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Recommend_Sys.Views;

namespace Recommend_Sys.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private object? _currentPage;

        public MainWindowViewModel()
        {
            Navigate("Home");
        }

        [RelayCommand]
        public void Navigate(string pageName)
        {
            switch (pageName)
            {
                case "Home":
                    CurrentPage = new HomePage();
                    break;
                case "Love":
                    CurrentPage = new LovePage();
                    break;
                case "Playground":
                    CurrentPage = new PlaygroundPage();
                    break;
                case "User":
                    CurrentPage = new UserPage();
                    break;
                default:
                    throw new ArgumentException("Invalid page name", nameof(pageName));
            }
        }
    }
}
