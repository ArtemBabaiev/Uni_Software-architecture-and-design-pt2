using DesktopClient.Data.Models;
using DesktopClient.Network.Requests;
using DesktopClient.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.Data.DAOs
{
    internal class PublisherDao
    {
        private ServerAcces _serverAcces;
        private readonly string API_Path = "/api/publishers";

        public PublisherDao()
        {
            _serverAcces = ServerAcces.GetAccess;
        }

        public async Task<List<Publisher>> GetAllPublishers()
        {
            return await _serverAcces.SendGet<List<Publisher>>(API_Path);
        }

        public async Task DeletePublisher(long id)
        {
            await _serverAcces.SendDelete(API_Path + $"/{id}");
        }

        public async Task<Publisher> GetPublisher(long id)
        {
            return await _serverAcces.SendGet<Publisher>(API_Path + $"/{id}");

        }

        public async Task<Publisher> UpdatePublisher(Publisher updatedPublisher)
        {
            PublisherPost post = new PublisherPost { Name = updatedPublisher.Name, Description = updatedPublisher.Description };
            return await _serverAcces.SendPut<Publisher>(API_Path + $"/{updatedPublisher.Id}", post);

        }

        public async Task<Publisher> CreatePublisher(PublisherPost publisher)
        {
            return await _serverAcces.SendPost<Publisher>(API_Path, publisher);

        }
    }
}
