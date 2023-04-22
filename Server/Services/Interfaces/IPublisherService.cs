using Server.DTOs.Publisher;
using Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Interfaces
{
    internal interface IPublisherService
    {
        Task<GetPublisherResponse> CreateAsync(CreatePublisherRequest request);
        Task<GetPublisherResponse> GetByIdAsync(long id);
        Task<IEnumerable<GetPublisherResponse>> GetAllAsync();
        Task<DeleteRespose> DeleteAsync(long id);
        Task<GetPublisherResponse> UpdateAsync(long id, UpdatePublisherRequest request);
    }
}
