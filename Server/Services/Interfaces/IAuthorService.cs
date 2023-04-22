using Server.DTOs.Author;
using Server.Network;

namespace Server.Services.Interfaces
{
    internal interface IAuthorService
    {
        Task<AuthorResponse> CreateAsync(AuthorCreateRequest request);
        Task<AuthorResponse> GetByIdAsync(long id);
        Task<IEnumerable<AuthorResponse>> GetAllAsync();
        Task<DeleteRespose> DeleteAsync(long id);
        Task<AuthorResponse> UpdateAsync(long id, AuthorUpdateRequest request);
    }
}
