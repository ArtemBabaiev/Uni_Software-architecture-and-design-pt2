using AutoMapper;
using Server.DTOs.Exemplar;
using Server.Models;
using Server.Network;
using Server.ObjectManagers;
using Server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    internal class ExemplarService: IExemplarService
    {
        MapperConfiguration mapConfig;

        public ExemplarService()
        {
            this.mapConfig = SingletonPool.MapperConfiguration;
        }

        public async Task<GetExemplarResponse> CreateAsync(CreateExemplarRequest request)
        {
            IMapper mapper = mapConfig.CreateMapper();
            using (var connection = ConnectionManager.GetSqlConnection())
            using (var uow = UowManager.GetUnitOfWork(connection))
            {
                var exemplar = mapper.Map<Exemplar>(request);
                exemplar.CreatedAt = DateTime.Now;
                exemplar.UpdatedAt = DateTime.Now;
                var newId = await uow.ExemplarRepository.AddAsync(exemplar);
                var newExemplar = await uow.ExemplarRepository.GetAsync(newId);
                uow.Commit();
                return mapper.Map<GetExemplarResponse>(newExemplar);

            }
        }

        public async Task<DeleteRespose> DeleteAsync(long id)
        {
            IMapper mapper = mapConfig.CreateMapper();
            using (var connection = ConnectionManager.GetSqlConnection())
            using (var uow = UowManager.GetUnitOfWork(connection))
            {
                var isDeleted = await uow.ExemplarRepository.DeleteAsync(id);
                uow.Commit();
                return new DeleteRespose() { IsDeleted = isDeleted };
            }
        }

        public async Task<IEnumerable<GetExemplarResponse>> GetAllAsync()
        {
            IMapper mapper = mapConfig.CreateMapper();
            using (var connection = ConnectionManager.GetSqlConnection())
            using (var uow = UowManager.GetUnitOfWork(connection))
            {
                var exemplars = await uow.ExemplarRepository.GetAllAsync();
                foreach (var exemplar in exemplars)
                {
                    exemplar.Book = await uow.BookRepository.GetAsync(exemplar.BookId);
                }
                uow.Commit();
                return exemplars?.Select(mapper.Map<Exemplar, GetExemplarResponse>);
            }
        }

        public async Task<GetExemplarResponse> GetByIdAsync(long id)
        {
            IMapper mapper = mapConfig.CreateMapper();
            using (var connection = ConnectionManager.GetSqlConnection())
            using (var uow = UowManager.GetUnitOfWork(connection))
            {
                var exemplar = await uow.ExemplarRepository.GetAsync(id);
                exemplar.Book = await uow.BookRepository.GetAsync(exemplar.BookId);
                uow.Commit();
                return mapper.Map<GetExemplarResponse>(exemplar);
            }
        }

        public async Task<GetExemplarResponse> UpdateAsync(long id, UpdateExemplarRequest request)
        {
            IMapper mapper = mapConfig.CreateMapper();
            using (var connection = ConnectionManager.GetSqlConnection())
            using (var uow = UowManager.GetUnitOfWork(connection))
            {
                var toUpdate = mapper.Map<Exemplar>(request);
                var inDb = await uow.ExemplarRepository.GetAsync(id);
                if (toUpdate != null)
                {
                    toUpdate.Id = id;
                    toUpdate.CreatedAt = inDb.CreatedAt;
                    toUpdate.UpdatedAt = DateTime.Now;
                    await uow.ExemplarRepository.ReplaceAsync(toUpdate);
                    var updated = await uow.ExemplarRepository.GetAsync(id);
                    uow.Commit();
                    return mapper.Map<GetExemplarResponse>(updated);
                }
                return null;
            }
        }
    }
}
