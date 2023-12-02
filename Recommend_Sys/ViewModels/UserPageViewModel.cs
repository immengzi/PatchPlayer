using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Recommend_Sys.Models;
using Recommend_Sys.Repositories;

namespace Recommend_Sys.ViewModels
{
    public partial class UserPageViewModel : ObservableObject
    {
        private IUserRepository userRepository;

        [ObservableProperty]
        private string? _displayName;

        public UserPageViewModel()
        {
            userRepository = new UserRepository();
            LoadCurrentUserData();
        }
        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                DisplayName = $"Welcome {user.Username}!";
            }
            else
            {
                DisplayName = "Invalid user, not logged in";
            }
        }
    }
}
