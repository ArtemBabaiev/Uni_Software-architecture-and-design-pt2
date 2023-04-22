using AutoMapper;
using Server.DTOs.Genre;
using Server.Models;
using Server.Network;
using Server.ObjectManagers;
using Server.Services.Interfaces;

namespace Server.Services
{
    internal class GenreService : IGenreService
    {
        MapperConfiguration mapConfig;

        public GenreService()
        {
            this.mapConfig = SingletonPool.MapperConfiguration;
        }

        public async Task<GetGenreResponse> CreateAsync(CreateGenreRequest request)
        {
            IMapper mapper = mapConfig.CreateMapper();
            using (var connection = ConnectionManager.GetSqlConnection())
            using (var uow = UowManager.GetUnitOfWork(connection))
            {
                var author = mapper.Map<Genre>(request);
                author.CreatedAt = DateTime.Now;
                author.UpdatedAt = DateTime.Now;
                var newId = await uow.GenreRepository.AddAsync(author);
                var newGenre = await uow.GenreRepository.GetAsync(newId);
                uow.Commit();
                return mapper.Map<GetGenreResponse>(newGenre);

            }
        }

        public async Task<DeleteRespose> DeleteAsync(long id)
        {
            IMapper mapper = mapConfig.CreateMapper();
            using (var connection = ConnectionManager.GetSqlConnection())
            using (var uow = UowManager.GetUnitOfWork(connection))
            {
                var isDeleted = await uow.GenreRepository.DeleteAsync(id);
                uow.Commit();
                return new DeleteRespose() { IsDeleted = isDeleted };
            }
        }

        public async Task<IEnumerable<GetGenreResponse>> GetAllAsync()
        {
            IMapper mapper = mapConfig.CreateMapper();
            using (var connection = ConnectionManager.GetSqlConnection())
            using (var uow = UowManager.GetUnitOfWork(connection))
            {
                var authors = await uow.GenreRepository.GetAllAsync();
                uow.Commit();
                return authors?.Select(mapper.Map<Genre, GetGenreResponse>);
            }
        }

        public async Task<GetGenreResponse> GetByIdAsync(long id)
        {
            IMapper mapper = mapConfig.CreateMapper();
            using (var connection = ConnectionManager.GetSqlConnection())
            using (var uow = UowManager.GetUnitOfWork(connection))
            {
                var author = await uow.GenreRepository.GetAsync(id);
                uow.Commit();
                return mapper.Map<GetGenreResponse>(author);
            }
        }

        public async Task<GetGenreResponse> UpdateAsync(long id, UpdateGenreRequest request)
        {
            IMapper mapper = mapConfig.CreateMapper();
            using (var connection = ConnectionManager.GetSqlConnection())
            using (var uow = UowManager.GetUnitOfWork(connection))
            {
                var toUpdate = mapper.Map<Genre>(request);
                var inDb = await uow.GenreRepository.GetAsync(id);
                if (toUpdate != null)
                {
                    toUpdate.Id = id;
                    toUpdate.CreatedAt = inDb.CreatedAt;
                    toUpdate.UpdatedAt = DateTime.Now;
                    await uow.GenreRepository.ReplaceAsync(toUpdate);
                    var updated = await uow.GenreRepository.GetAsync(id);
                    uow.Commit();
                    return mapper.Map<GetGenreResponse>(updated);
                }
                return null;
            }
        }
    }
}
