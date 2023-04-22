using Server.DTOs.Genre;
using Server.Network;

namespace Server.Services.Interfaces
{
    internal interface IGenreService
    {
        Task<GetGenreResponse> CreateAsync(CreateGenreRequest request);
        Task<GetGenreResponse> GetByIdAsync(long id);
        Task<IEnumerable<GetGenreResponse>> GetAllAsync();
        Task<DeleteRespose> DeleteAsync(long id);
        Task<GetGenreResponse> UpdateAsync(long id, UpdateGenreRequest request);
    }
}
