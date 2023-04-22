using AutoMapper;
using Server.DTOs.Book;
using Server.Models;
using Server.Network;
using Server.ObjectManagers;
using Server.Services.Interfaces;

namespace Server.Services
{
    internal class BookService : IBookService
    {
        MapperConfiguration mapConfig;

        public BookService()
        {
            this.mapConfig = SingletonPool.MapperConfiguration;
        }

        public async Task<GetBookResponse> CreateAsync(CreateBookRequest request)
        {
            IMapper mapper = mapConfig.CreateMapper();
            using (var connection = ConnectionManager.GetSqlConnection())
            using (var uow = UowManager.GetUnitOfWork(connection))
            {
                var book = mapper.Map<Book>(request);
                book.CreatedAt = DateTime.Now;
                book.UpdatedAt = DateTime.Now;
                var newId = await uow.BookRepository.AddAsync(book);
                var newBook = await uow.BookRepository.GetAsync(newId);
                uow.Commit();
                return mapper.Map<GetBookResponse>(newBook);
            }
        }

        public async Task<DeleteRespose> DeleteAsync(long id)
        {
            IMapper mapper = mapConfig.CreateMapper();
            using (var connection = ConnectionManager.GetSqlConnection())
            using (var uow = UowManager.GetUnitOfWork(connection))
            {
                var isDeleted = await uow.BookRepository.DeleteAsync(id);
                uow.Commit();
                return new DeleteRespose() { IsDeleted = isDeleted };
            }
        }

        public async Task<IEnumerable<GetBookResponse>> GetAllAsync()
        {
            IMapper mapper = mapConfig.CreateMapper();
            using (var connection = ConnectionManager.GetSqlConnection())
            using (var uow = UowManager.GetUnitOfWork(connection))
            {
                var books = await uow.BookRepository.GetAllAsync();
                foreach (var book in books)
                {
                    book.Author = await uow.AuthorRepository.GetAsync(book.AuthorId);
                    book.Genre = await uow.GenreRepository.GetAsync(book.GenreId);
                    book.Publisher = await uow.PublisherRepository.GetAsync(book.PublisherId);
                }
                uow.Commit();
                return books?.Select(mapper.Map<Book, GetBookResponse>);
            }
        }

        public async Task<GetBookResponse> GetByIdAsync(long id)
        {
            IMapper mapper = mapConfig.CreateMapper();
            using (var connection = ConnectionManager.GetSqlConnection())
            using (var uow = UowManager.GetUnitOfWork(connection))
            {
                var book = await uow.BookRepository.GetAsync(id);
                book.Author = await uow.AuthorRepository.GetAsync(book.AuthorId);
                book.Genre = await uow.GenreRepository.GetAsync(book.GenreId);
                book.Publisher = await uow.PublisherRepository.GetAsync(book.PublisherId);
                uow.Commit();
                return mapper.Map<GetBookResponse>(book);
            }
        }

        public async Task<GetBookResponse> UpdateAsync(long id, UpdateBookRequest request)
        {
            IMapper mapper = mapConfig.CreateMapper();
            using (var connection = ConnectionManager.GetSqlConnection())
            using (var uow = UowManager.GetUnitOfWork(connection))
            {
                var toUpdate = mapper.Map<Book>(request);
                var inDb = await uow.BookRepository.GetAsync(id);
                if (toUpdate != null)
                {
                    toUpdate.Id = id;
                    toUpdate.CreatedAt = inDb.CreatedAt;
                    toUpdate.UpdatedAt = DateTime.Now;
                    await uow.BookRepository.ReplaceAsync(toUpdate);
                    var updated = await uow.BookRepository.GetAsync(id);
                    uow.Commit();
                    return mapper.Map<GetBookResponse>(updated);
                }
                return null;
            }
        }
    }
}
