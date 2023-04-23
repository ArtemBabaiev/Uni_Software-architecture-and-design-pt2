using DesktopClient.Data.DAOs;
using DesktopClient.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.ViewModels
{
    internal class PublisherTableVM
    {
        private static PublisherDao publisherDao = new();

        private ObservableCollection<Publisher> _publisherList;
        public ObservableCollection<Publisher> PublisherList { get => _publisherList; set => _publisherList = value; }

        public PublisherTableVM()
        {
            PublisherList = new ObservableCollection<Publisher>();
        }

        public async Task GetPublishers()
        {
            ClearList();
            var publishers = await publisherDao.GetAllPublishers();
            foreach (var item in publishers)
            {
                PublisherList.Add(item);
            }
        }

        public async Task DeletePublisher(long id)
        {
            await publisherDao.DeletePublisher(id);
            await GetPublishers();
        }

        public async Task UpdatePublisher(long id)
        {
            var toUpdate = PublisherList.First(auth => auth.Id == id);
            if (toUpdate == null)
            {
                return;
            }
            Publisher updated = await publisherDao.UpdatePublisher(toUpdate);
            var insertionIndex = PublisherList.IndexOf(toUpdate);
            PublisherList.Remove(toUpdate);
            PublisherList.Insert(insertionIndex, updated);
        }

        public void ClearList()
        {
            PublisherList.Clear();
        }
    }
}
