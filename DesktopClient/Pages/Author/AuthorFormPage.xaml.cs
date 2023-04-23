using DesktopClient.Utils;
using DesktopClient.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace DesktopClient.Pages.Author
{
    /// <summary>
    /// Interaction logic for AuthorFormPage.xaml
    /// </summary>
    public partial class AuthorFormPage : Page
    {
        AuthorFormVM viewModel;
        public AuthorFormPage()
        {
            viewModel = ViewModelFactory.GetViewModel<AuthorFormVM>();
            DataContext = viewModel;
            InitializeComponent();
        }


        private async void OnSaveButtonClick(object sender, RoutedEventArgs e)
        {
            await viewModel.SaveAuthor();
            NavigationService.GoBack();
        }

        private void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
