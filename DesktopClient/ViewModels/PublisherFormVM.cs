using DesktopClient.Data.DAOs;
using DesktopClient.Network.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.ViewModels
{
    internal class PublisherFormVM
    {
        private static PublisherDao publisherDao = new();

        private PublisherPost _publisher;

        public PublisherFormVM()
        {
            Publisher = new PublisherPost();
        }

        public PublisherPost Publisher
        {
            get => _publisher;
            set
            {
                _publisher = value;
                OnPropertyChanged(nameof(Publisher));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public async Task SavePublisher()
        {
            await publisherDao.CreatePublisher(_publisher);
            _publisher = new();
        }   
    }
}
