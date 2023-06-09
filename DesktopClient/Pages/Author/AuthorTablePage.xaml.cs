﻿using DesktopClient.Utils;
using DesktopClient.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

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
            viewModel = ViewModelFactory.GetViewModel<AuthorTableVM>();
            DataContext = viewModel;
            InitializeComponent();
        }

        public async Task LoadTable()
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
            //NavigationService.Navigate(new AuthorFormPage());
            NavigationService.Navigate(new Uri("Pages/Author/AuthorFormPage.xaml", UriKind.Relative));
            viewModel.ClearList();
        }

        private async void OnSaveClick(object sender, RoutedEventArgs e)
        {
            var tag = ((Button)sender).Tag;
            await viewModel.UpdateAuthor(Convert.ToInt64(tag));
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
