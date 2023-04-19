using Server.DTOs;
using Server.DTOs.Author;

namespace Server.Services.Interfaces
{
    internal interface IAuthorService
    {
        Task<AuthorResponse> CreateAuthor(AuthorCreateRequest request);
        Task<AuthorResponse> GetAuthorById(long id);
        Task<IEnumerable<AuthorResponse>> GetAllAuthors();
        Task<DeleteRespose> DeleteAuthor(long id);
        Task<AuthorResponse> UpdateAuthor(long id, AuthorUpdateRequest request);
    }
}
