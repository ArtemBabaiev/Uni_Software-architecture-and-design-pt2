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
    /// Interaction logic for BookFormPage.xaml
    /// </summary>
    public partial class BookFormPage : Page
    {
        BookFormVM viewModel;
        public BookFormPage()
        {
            viewModel = ViewModelFactory.GetViewModel<BookFormVM>();
            DataContext = viewModel;
            InitializeComponent();
        }


        private async void OnSaveButtonClick(object sender, RoutedEventArgs e)
        {
            await viewModel.SaveBook();
            NavigationService.GoBack();
        }

        private void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
