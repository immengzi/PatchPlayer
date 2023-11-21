using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using FlyleafLib;

namespace Recommend_Sys
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Engine.Start(
                new EngineConfig()
                {
                    FFmpegPath = @"C:\Flyleaf\FFmpeg",
                    FFmpegDevices = false, // Prevents loading avdevice/avfilter dll files. Enable it only if you plan to use dshow/gdigrab etc.
#if RELEASE
                    FFmpegLogLevel = FFmpegLogLevel.Quiet,
                    LogLevel = LogLevel.Quiet,
#else
                    FFmpegLogLevel = FFmpegLogLevel.Warning,
                    LogLevel = LogLevel.Debug,
                    LogOutput = ":debug",
                    //LogOutput         = ":console",
                    //LogOutput         = @"C:\Flyleaf\Logs\flyleaf.log",
#endif

                    //PluginsPath       = @"C:\Flyleaf\Plugins",

                    UIRefresh = false, // Required for Activity, BufferedDuration, Stats in combination with Config.Player.Stats = true
                    UIRefreshInterval = 250, // How often (in ms) to notify the UI
                    UICurTimePerSecond = true, // Whether to notify UI for CurTime only when it's second changed or by UIRefreshInterval
                }
            );
        }
    }
}
