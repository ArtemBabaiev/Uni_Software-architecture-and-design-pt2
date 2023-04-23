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

namespace DesktopClient.Pages.Exemplar
{
    /// <summary>
    /// Interaction logic for ExemplarFormPage.xaml
    /// </summary>
    public partial class ExemplarFormPage : Page
    {
        ExemplarFormVM viewModel;
        public ExemplarFormPage()
        {
            viewModel = ViewModelFactory.GetViewModel<ExemplarFormVM>();
            DataContext = viewModel;
            InitializeComponent();
        }


        private async void OnSaveButtonClick(object sender, RoutedEventArgs e)
        {
            await viewModel.SaveExemplar();
            NavigationService.GoBack();
        }

        private void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
