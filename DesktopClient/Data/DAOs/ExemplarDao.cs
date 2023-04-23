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
    internal class ExemplarDao
    {
        private ServerAcces _serverAcces;
        private readonly string API_Path = "/api/exemplars";

        public ExemplarDao()
        {
            _serverAcces = ServerAcces.GetAccess;
        }

        public async Task<List<Exemplar>> GetAllExemplars()
        {
            return await _serverAcces.SendGet<List<Exemplar>>(API_Path);
        }

        public async Task DeleteExemplar(long id)
        {
            await _serverAcces.SendDelete(API_Path + $"/{id}");
        }

        public async Task<Exemplar> GetExemplar(long id)
        {
            return await _serverAcces.SendGet<Exemplar>(API_Path + $"/{id}");

        }

        public async Task<Exemplar> UpdateExemplar(Exemplar updatedExemplar)
        {
            ExemplarPost post = new ExemplarPost { BookId = updatedExemplar.BookId, IsLend = updatedExemplar.IsLend };
            return await _serverAcces.SendPut<Exemplar>(API_Path + $"/{updatedExemplar.Id}", post);
        }

        public async Task<Exemplar> CreateExemplar(ExemplarPost exemplar)
        {
            return await _serverAcces.SendPost<Exemplar>(API_Path, exemplar);

        }
    }
}
