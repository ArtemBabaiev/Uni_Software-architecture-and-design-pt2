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

namespace DesktopClient.Pages.Publisher
{
    /// <summary>
    /// Interaction logic for PublisherTablePage.xaml
    /// </summary>
    public partial class PublisherTablePage : Page
    {
        static PublisherTableVM viewModel;
        public PublisherTablePage()
        {
            viewModel = ViewModelFactory.GetViewModel<PublisherTableVM>();
            DataContext = viewModel;
            InitializeComponent();
        }

        public async Task LoadTable()
        {
            await viewModel.GetPublishers();
        }

        private async void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            var tag = ((Button)sender).Tag;
            await viewModel.DeletePublisher(Convert.ToInt64(tag));
        }

        private void OnCreateButtonClick(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new PublisherFormPage());
            NavigationService.Navigate(new Uri("Pages/Publisher/PublisherFormPage.xaml", UriKind.Relative));
            viewModel.ClearList();
        }

        private async void OnSaveClick(object sender, RoutedEventArgs e)
        {
            var tag = ((Button)sender).Tag;
            await viewModel.UpdatePublisher(Convert.ToInt64(tag));
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
