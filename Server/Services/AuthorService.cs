using AutoMapper;
using Server.DTOs.Author;
using Server.Models;
using Server.Network;
using Server.ObjectManagers;
using Server.Services.Interfaces;

namespace Server.Services
{
    internal class AuthorService : IAuthorService
    {
        MapperConfiguration mapConfig;

        public AuthorService()
        {
            this.mapConfig = SingletonPool.MapperConfiguration;
        }

        public async Task<AuthorResponse> CreateAsync(AuthorCreateRequest request)
        {
            IMapper mapper = mapConfig.CreateMapper();
            using (var connection = ConnectionManager.GetSqlConnection())
            using (var uow = UowManager.GetUnitOfWork(connection))
            {
                var author = mapper.Map<Author>(request);
                author.CreatedAt = DateTime.Now;
                author.UpdatedAt = DateTime.Now;
                var newId = await uow.AuthorRepository.AddAsync(author);
                var newAuthor = await uow.AuthorRepository.GetAsync(newId);
                uow.Commit();
                return mapper.Map<AuthorResponse>(newAuthor);

            }
        }

        public async Task<DeleteRespose> DeleteAsync(long id)
        {
            IMapper mapper = mapConfig.CreateMapper();
            using (var connection = ConnectionManager.GetSqlConnection())
            using (var uow = UowManager.GetUnitOfWork(connection))
            {
                var isDeleted = await uow.AuthorRepository.DeleteAsync(id);
                uow.Commit();
                return new DeleteRespose() { IsDeleted = isDeleted };
            }
        }

        public async Task<IEnumerable<AuthorResponse>> GetAllAsync()
        {
            IMapper mapper = mapConfig.CreateMapper();
            using (var connection = ConnectionManager.GetSqlConnection())
            using (var uow = UowManager.GetUnitOfWork(connection))
            {
                var authors = await uow.AuthorRepository.GetAllAsync();
                uow.Commit();
                return authors?.Select(mapper.Map<Author, AuthorResponse>);
            }
        }

        public async Task<AuthorResponse> GetByIdAsync(long id)
        {
            IMapper mapper = mapConfig.CreateMapper();
            using (var connection = ConnectionManager.GetSqlConnection())
            using (var uow = UowManager.GetUnitOfWork(connection))
            {
                var author = await uow.AuthorRepository.GetAsync(id);
                uow.Commit();
                return mapper.Map<AuthorResponse>(author);
            }
        }

        public async Task<AuthorResponse> UpdateAsync(long id, AuthorUpdateRequest request)
        {
            IMapper mapper = mapConfig.CreateMapper();
            using (var connection = ConnectionManager.GetSqlConnection())
            using (var uow = UowManager.GetUnitOfWork(connection))
            {
                var toUpdate = mapper.Map<Author>(request);
                var inDb = await uow.AuthorRepository.GetAsync(id);
                if (toUpdate != null)
                {
                    toUpdate.Id = id;
                    toUpdate.CreatedAt = inDb.CreatedAt;
                    toUpdate.UpdatedAt = DateTime.Now;
                    await uow.AuthorRepository.ReplaceAsync(toUpdate);
                    var updated = await uow.AuthorRepository.GetAsync(id);
                    uow.Commit();
                    return mapper.Map<AuthorResponse>(updated);
                }
                return null;
            }
        }
    }
}
