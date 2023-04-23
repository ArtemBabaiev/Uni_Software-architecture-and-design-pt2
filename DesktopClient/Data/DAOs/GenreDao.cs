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
    internal class GenreDao
    {
        private ServerAcces _serverAcces;
        private readonly string API_Path = "/api/genres";

        public GenreDao()
        {
            _serverAcces = ServerAcces.GetAccess;
        }

        public async Task<List<Genre>> GetAllGenres()
        {
            return await _serverAcces.SendGet<List<Genre>>(API_Path);
        }

        public async Task DeleteGenre(long id)
        {
            await _serverAcces.SendDelete(API_Path + $"/{id}");
        }

        public async Task<Genre> GetGenre(long id)
        {
            return await _serverAcces.SendGet<Genre>(API_Path + $"/{id}");

        }

        public async Task<Genre> UpdateGenre(Genre updatedGenre)
        {
            GenrePost post = new GenrePost { Name = updatedGenre.Name, Description = updatedGenre.Description };
            return await _serverAcces.SendPut<Genre>(API_Path + $"/{updatedGenre.Id}", post);
        }

        public async Task<Genre> CreateGenre(GenrePost genre)
        {
            return await _serverAcces.SendPost<Genre>(API_Path, genre);

        }
    }
}
