using DesktopClient.Dao;
using DesktopClient.ViewModel;
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

namespace DesktopClient.Pages.Author
{
    /// <summary>
    /// Interaction logic for AuthorsTable.xaml
    /// </summary>
    public partial class AuthorTablePage : Page
    {
        static AuthorTableVM viewModel;
        public AuthorTablePage()
        {
            viewModel = new AuthorTableVM();
            DataContext = viewModel;
            InitializeComponent();
            //LoadTable();
        }

        public async void LoadTable()
        {
            await viewModel.GetAuthors();
        }


        private async void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            var tag = ((Button)sender).Tag;
            await viewModel.DeleteAuthor(Convert.ToInt64(tag));
        }

        private void OnCreateButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorFormPage());
        }

        private async void OnSaveClick(object sender, RoutedEventArgs e)
        {
            var tag = ((Button)sender).Tag;
            await viewModel.UpdateAuthor(Convert.ToInt64(tag));
        }

        private async void OnRefreshButtonClick(object sender, RoutedEventArgs e)
        {
            LoadTable();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            LoadTable();
        }
    }
}
