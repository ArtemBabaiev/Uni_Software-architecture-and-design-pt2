using Server.DTOs.Exemplar;
using Server.Network;

namespace Server.Services.Interfaces
{
    internal interface IExemplarService
    {
        Task<GetExemplarResponse> CreateAsync(CreateExemplarRequest request);
        Task<GetExemplarResponse> GetByIdAsync(long id);
        Task<IEnumerable<GetExemplarResponse>> GetAllAsync();
        Task<DeleteRespose> DeleteAsync(long id);
        Task<GetExemplarResponse> UpdateAsync(long id, UpdateExemplarRequest request);
    }
}
