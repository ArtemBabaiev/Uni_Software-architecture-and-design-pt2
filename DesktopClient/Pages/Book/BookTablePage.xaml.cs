using DesktopClient.Utils;
using DesktopClient.ViewModels;
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

namespace DesktopClient.Pages.Book
{
    /// <summary>
    /// Interaction logic for BookTablePage.xaml
    /// </summary>
    public partial class BookTablePage : Page
    {
        static BookTableVM viewModel;
        public BookTablePage()
        {
            viewModel = ViewModelFactory.GetViewModel<BookTableVM>();
            DataContext = viewModel;
            InitializeComponent();
        }

        public async Task LoadTable()
        {
            await viewModel.GetBooks();
        }

        private async void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            var tag = ((Button)sender).Tag;
            await viewModel.DeleteBook(Convert.ToInt64(tag));
        }

        private void OnCreateButtonClick(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new BookFormPage());
            NavigationService.Navigate(new Uri("Pages/Book/BookFormPage.xaml", UriKind.Relative));
            viewModel.ClearList();
        }

        private async void OnSaveClick(object sender, RoutedEventArgs e)
        {
            var tag = ((Button)sender).Tag;
            await viewModel.UpdateBook(Convert.ToInt64(tag));
        }

        private async void OnRefreshButtonClick(object sender, RoutedEventArgs e)
        {
            await LoadTable();
        }

        private async void OnLoad(object sender, RoutedEventArgs e)
        {
            await LoadTable();
        }

        private void OnHomeClick(object sender, RoutedEventArgs e)
        {
            viewModel.ClearList();
            NavigationService.Navigate(new Uri("/Pages/StartScreenPage.xaml", UriKind.Relative));
        }
    }
}
