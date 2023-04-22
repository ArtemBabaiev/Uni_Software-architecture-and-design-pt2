using DesktopClient.Exceptions;
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
    /// Interaction logic for AuthorFormPage.xaml
    /// </summary>
    public partial class AuthorFormPage : Page
    {
        AuthorFormVM viewModel;
        public AuthorFormPage()
        {
            viewModel = new();
            InitializeComponent();
            DataContext = viewModel;
        }


        private async void OnSaveClick(object sender, RoutedEventArgs e)
        {

            await viewModel.SaveAuthor();
            NavigationService.GoBack();


        }
    }
}
