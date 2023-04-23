using DesktopClient.Pages;
using DesktopClient.Pages.Author;
using DesktopClient.Pages.Book;
using DesktopClient.Pages.Exemplar;
using DesktopClient.Pages.Genre;
using DesktopClient.Pages.Publisher;
using System;
using System.Windows;
using System.Windows.Navigation;

namespace DesktopClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainContent.Content = new StartScreenPage();
        }
    }
}
