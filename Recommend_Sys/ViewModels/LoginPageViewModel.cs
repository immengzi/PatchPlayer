using Recommend_Sys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Recommend_Sys.Repositories;
using System.Net;
using System.Security.Principal;
using System.Threading;

namespace Recommend_Sys.ViewModels
{
    public partial class LoginPageViewModel : ObservableObject
    {
        private IUserRepository userRepository;

        [ObservableProperty]
        private string? _username;

        [ObservableProperty]
        private string? _password;

        [ObservableProperty]
        private string? _errorMessage;

        [ObservableProperty]
        private bool _isViewVisible;

        [RelayCommand]
        private void Login()
        {
            bool isValidInput = IsValidInput();

            if (LoginCommand is RelayCommand relayCommand)
            {
                relayCommand.CanExecute(isValidInput); 
            }

            if (isValidInput)
            {
                var isValidUser = userRepository.AuthenticateUser(new NetworkCredential(Username, Password));
                if (isValidUser)
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(
                        new GenericIdentity(Username), null);
                    IsViewVisible = false;
                }
                else
                {
                    ErrorMessage = "* Invalid username or password";
                }
            }
        }

        private bool IsValidInput()
        {
            return !string.IsNullOrWhiteSpace(Username) && Username.Length >= 3 &&
                   Password != null && Password.Length >= 3;
        }

        public LoginPageViewModel()
        {
            userRepository = new UserRepository();
            Login();
        }
    }
}
