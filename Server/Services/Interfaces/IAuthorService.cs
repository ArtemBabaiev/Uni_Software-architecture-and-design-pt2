using Server.DTOs.Author;
using Server.Network;

namespace Server.Services.Interfaces
{
    internal interface IAuthorService
    {
        Task<GetAuthorResponse> CreateAsync(CreateAuthorRequest request);
        Task<GetAuthorResponse> GetByIdAsync(long id);
        Task<IEnumerable<GetAuthorResponse>> GetAllAsync();
        Task<DeleteRespose> DeleteAsync(long id);
        Task<GetAuthorResponse> UpdateAsync(long id, UpdateAuthorRequest request);
    }
}
