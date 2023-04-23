using DesktopClient.Pages.Book;
using DesktopClient.Pages.Exemplar;
using DesktopClient.Pages.Genre;
using DesktopClient.Pages.Publisher;
using System;
using System.Collections.Generic;
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

namespace DesktopClient.Pages
{
    /// <summary>
    /// Interaction logic for StartScreenPage.xaml
    /// </summary>
    public partial class StartScreenPage : Page
    {
        public StartScreenPage()
        {
            InitializeComponent();
        }

        private void OnAuthorPageButtonClick(object sender, RoutedEventArgs e)
        {
            ClearNavigation();
            NavigationService.Navigate(new Uri("/Pages/Author/AuthorTablePage.xaml", UriKind.Relative));
        }

        private void OnBookPageButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Book/BookTablePage.xaml", UriKind.Relative)); 
        }

        private void OnExemplarPageButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Exemplar/ExemplarTablePage.xaml", UriKind.Relative)); 
        }

        private void OnGenrePageButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Genre/GenreTablePage.xaml", UriKind.Relative)); 
        }

        private void OnPublisherPageButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Publisher/PublisherTablePage.xaml", UriKind.Relative)); 
        }

        private void ClearNavigation()
        {
            while (true)
            {
                var back = NavigationService.RemoveBackEntry();
                if (back == null)
                {
                    break;
                }
            }
        }
    }
}
