using Server.DTOs.Author;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Interfaces
{
    internal interface IAuthorService
    {
        Task<AuthorResponse> CreateAuthor(AuthorCreateRequest request);
        Task<AuthorResponse> GetAuthorById(long id);
        Task<IEnumerable<AuthorResponse>> GetAllAuthors();
        Task DeleteAuthor(long id);
        Task<AuthorResponse> UpdateAuthor(long id, AuthorUpdateRequest request);
    }
}
