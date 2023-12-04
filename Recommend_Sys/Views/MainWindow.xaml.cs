using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Recommend_Sys.Services;
using Recommend_Sys.ViewModels;
using Recommend_Sys.Views;

namespace Recommend_Sys
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindowViewModel)?.Navigate("Home");
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindowViewModel)?.Navigate("Love");
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindowViewModel)?.Navigate("Playground");
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindowViewModel)?.Navigate("User");
        }

    }
}
