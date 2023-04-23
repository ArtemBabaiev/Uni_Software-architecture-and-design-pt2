using DesktopClient.Data.Models;
using DesktopClient.Exceptions;
using DesktopClient.Network;
using DesktopClient.Network.Requests;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DesktopClient.Data.DAOs
{
    internal class AuthorDao
    {
        private ServerAcces _serverAcces;
        private readonly string API_Path = "/api/authors";

        public AuthorDao()
        {
            _serverAcces = ServerAcces.GetAccess;
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            return await _serverAcces.SendGet<List<Author>>(API_Path);
        }

        public async Task DeleteAuthor(long id)
        {
             await _serverAcces.SendDelete(API_Path + $"/{id}");
        }

        public async Task<Author> GetAuthor(long id)
        {
            return await _serverAcces.SendGet<Author>(API_Path + $"/{id}").ConfigureAwait(false);
            
        }

        public async Task<Author> UpdateAuthor(Author updatedAuthor)
        {
            AuthorPost post = new AuthorPost { Name = updatedAuthor.Name, Description = updatedAuthor.Description };
            return await _serverAcces.SendPut<Author>(API_Path + $"/{updatedAuthor.Id}", post);

        }

        public async Task<Author> CreateAuthor(AuthorPost author)
        {
            return await _serverAcces.SendPost<Author>(API_Path, author);

        }
    }
}
