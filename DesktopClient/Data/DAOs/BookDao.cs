using DesktopClient.Data.Models;
using DesktopClient.Exceptions;
using DesktopClient.Network.Requests;
using DesktopClient.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DesktopClient.Data.DAOs
{
    internal class BookDao
    {
        private ServerAcces _serverAcces;
        private readonly string API_Path = "/api/books";

        public BookDao()
        {
            _serverAcces = ServerAcces.GetAccess;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _serverAcces.SendGet<List<Book>>(API_Path);
        }

        public async Task DeleteBook(long id)
        {
            await _serverAcces.SendDelete(API_Path + $"/{id}");
        }

        public async Task<Book> GetBook(long id)
        {
            return await _serverAcces.SendGet<Book>(API_Path + $"/{id}");

        }

        public async Task<Book> UpdateBook(Book updatedBook)
        {
            BookPost post = new BookPost { 
                Name = updatedBook.Name,
                Description = updatedBook.Description,
                Isbn = updatedBook.Isbn,
                NumberOfPages = updatedBook.NumberOfPages,
                PublishingYear = updatedBook.PublishingYear,
                AuthorId = updatedBook.AuthorId,
                GenreId = updatedBook.GenreId,
                PublisherId = updatedBook.PublisherId
            };
            return await _serverAcces.SendPut<Book>(API_Path + $"/{updatedBook.Id}", post);
        }

        public async Task<Book> CreateBook(BookPost book)
        {
            return await _serverAcces.SendPost<Book>(API_Path, book);

        }
    }
}
