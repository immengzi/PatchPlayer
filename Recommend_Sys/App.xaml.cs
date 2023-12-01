using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Recommend_Sys.Views;

namespace Recommend_Sys
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            var loginPage = new LoginPage();
            loginPage.Show();
            loginPage.IsVisibleChanged += (s, ev) =>
            {
                if (loginPage.IsVisible == false && loginPage.IsLoaded)
                {
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                    loginPage.Close();
                }
            };
        }
    }
}
