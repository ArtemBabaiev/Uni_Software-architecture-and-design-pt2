using DesktopClient.Entities;
using DesktopClient.Exceptions;
using DesktopClient.Requests;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DesktopClient.Dao
{
    internal class AuthorDao
    {
        private HttpClient _httpClient;
        private readonly string API_Path = "/api/authors";

        public AuthorDao()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("ServerHost"));
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            var response = await _httpClient.GetAsync(API_Path).ConfigureAwait(false);
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonSerializer.Deserialize<List<Author>>(responseBody);
                default:
                    throw new HttpResponseException();
            }
        }

        public async Task DeleteAuthor(long id)
        {
            var response = await _httpClient.DeleteAsync(API_Path + $"/{id}");
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return;
                default:
                    throw new HttpResponseException();
            }
        }
        
        public async Task<Author> GetAuthor(long id)
        {
            var response = await _httpClient.GetAsync(API_Path + $"/{id}").ConfigureAwait(false);
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonSerializer.Deserialize<Author>(responseBody);
                default:
                    throw new HttpResponseException();
            }
        }

        public async Task<Author> UpdateAuthor(Author updatedAuthor)
        {
            var json = JsonSerializer.Serialize(updatedAuthor);
            var requestContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(API_Path + $"/{updatedAuthor.Id}", requestContent).ConfigureAwait(false);
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonSerializer.Deserialize<Author>(responseBody);
                default:
                    throw new HttpResponseException();
            }
        }

        public async Task<Author> CreateAuthor(AuthorPost author)
        {
            var json = JsonSerializer.Serialize(author);
            var requestContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(API_Path, requestContent).ConfigureAwait(false);
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonSerializer.Deserialize<Author>(responseBody);
                default:
                    throw new HttpResponseException();
            }
            
        }


    }
}
