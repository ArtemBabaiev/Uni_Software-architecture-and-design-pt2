using AutoMapper;
using Server.DTOs.Publisher;
using Server.Models;
using Server.Network;
using Server.ObjectManagers;
using Server.Services.Interfaces;

namespace Server.Services
{
    internal class PublisherService : IPublisherService
    {
        MapperConfiguration mapConfig;

        public PublisherService()
        {
            this.mapConfig = SingletonPool.MapperConfiguration;
        }

        public async Task<GetPublisherResponse> CreateAsync(CreatePublisherRequest request)
        {
            IMapper mapper = mapConfig.CreateMapper();
            using (var connection = ConnectionManager.GetSqlConnection())
            using (var uow = UowManager.GetUnitOfWork(connection))
            {
                var author = mapper.Map<Publisher>(request);
                author.CreatedAt = DateTime.Now;
                author.UpdatedAt = DateTime.Now;
                var newId = await uow.PublisherRepository.AddAsync(author);
                var newPublisher = await uow.PublisherRepository.GetAsync(newId);
                uow.Commit();
                return mapper.Map<GetPublisherResponse>(newPublisher);

            }
        }

        public async Task<DeleteRespose> DeleteAsync(long id)
        {
            IMapper mapper = mapConfig.CreateMapper();
            using (var connection = ConnectionManager.GetSqlConnection())
            using (var uow = UowManager.GetUnitOfWork(connection))
            {
                var isDeleted = await uow.PublisherRepository.DeleteAsync(id);
                uow.Commit();
                return new DeleteRespose() { IsDeleted = isDeleted };
            }
        }

        public async Task<IEnumerable<GetPublisherResponse>> GetAllAsync()
        {
            IMapper mapper = mapConfig.CreateMapper();
            using (var connection = ConnectionManager.GetSqlConnection())
            using (var uow = UowManager.GetUnitOfWork(connection))
            {
                var authors = await uow.PublisherRepository.GetAllAsync();
                uow.Commit();
                return authors?.Select(mapper.Map<Publisher, GetPublisherResponse>);
            }
        }

        public async Task<GetPublisherResponse> GetByIdAsync(long id)
        {
            IMapper mapper = mapConfig.CreateMapper();
            using (var connection = ConnectionManager.GetSqlConnection())
            using (var uow = UowManager.GetUnitOfWork(connection))
            {
                var author = await uow.PublisherRepository.GetAsync(id);
                uow.Commit();
                return mapper.Map<GetPublisherResponse>(author);
            }
        }

        public async Task<GetPublisherResponse> UpdateAsync(long id, UpdatePublisherRequest request)
        {
            IMapper mapper = mapConfig.CreateMapper();
            using (var connection = ConnectionManager.GetSqlConnection())
            using (var uow = UowManager.GetUnitOfWork(connection))
            {
                var toUpdate = mapper.Map<Publisher>(request);
                var inDb = await uow.PublisherRepository.GetAsync(id);
                if (toUpdate != null)
                {
                    toUpdate.Id = id;
                    toUpdate.CreatedAt = inDb.CreatedAt;
                    toUpdate.UpdatedAt = DateTime.Now;
                    await uow.PublisherRepository.ReplaceAsync(toUpdate);
                    var updated = await uow.PublisherRepository.GetAsync(id);
                    uow.Commit();
                    return mapper.Map<GetPublisherResponse>(updated);
                }
                return null;
            }
        }
    }
}
