using Server.DTOs.Book;
using Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Interfaces
{
    internal interface IBookService
    {
        Task<GetBookResponse> CreateAsync(CreateBookRequest request);
        Task<GetBookResponse> GetByIdAsync(long id);
        Task<IEnumerable<GetBookResponse>> GetAllAsync();
        Task<DeleteRespose> DeleteAsync(long id);
        Task<GetBookResponse> UpdateAsync(long id, UpdateBookRequest request);
    }
}
