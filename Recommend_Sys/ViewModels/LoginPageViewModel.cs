using Recommend_Sys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using Recommend_Sys.Repositories;

namespace Recommend_Sys.ViewModels
{
    public partial class LoginPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? _username;

        [ObservableProperty]
        private string? _password;

        private IUserRepository userRepository;

        public LoginPageViewModel()
        {
            userRepository = new UserRepository();
        }
    }
}
