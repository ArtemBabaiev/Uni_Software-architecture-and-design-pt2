using DesktopClient.Data.Models;
using DesktopClient.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DesktopClient.Network
{
    internal class ServerAcces
    {
        private HttpClient _httpClient;
        private static readonly object _lock = new object();
        private static ServerAcces _instanse;

        internal static ServerAcces GetAccess
        {
            get
            {
                if (_instanse == null)
                {
                    lock (_lock)
                    {
                        if (_instanse == null)
                        {
                            _instanse = new ServerAcces();
                        }
                    }
                }

                return _instanse;
            }
        }

        private ServerAcces()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("ServerHost"));
        }

        public async Task<T> SendGet<T>(string url) where T : class
        {
            var response = await _httpClient.GetAsync(url);
            return ProcessRespose<T>(response);
        }

        public async Task<T> SendPost<T>(string url, object bodyObj) where T : class
        {
            var content = PackBody(bodyObj);
            var response = await _httpClient.PostAsync(url, content);
            return ProcessRespose<T>(response);

        }

        public async Task<T> SendPut<T>(string url, object bodyObj) where T : class
        {
            var content = PackBody(bodyObj);
            var response = await _httpClient.PutAsync(url, content);
            return ProcessRespose<T>(response);

        }

        public async Task SendDelete(string url)
        {
            await _httpClient.DeleteAsync(url);
        }

        private T ProcessRespose<T>(HttpResponseMessage response) where T : class
        {
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    return JsonSerializer.Deserialize<T>(responseBody);
                default:
                    throw new HttpResponseException();
            }
        }

        private HttpContent PackBody(object bodyObj)
        {
            var json = JsonSerializer.Serialize(bodyObj);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
