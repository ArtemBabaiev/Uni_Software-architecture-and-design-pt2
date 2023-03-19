using Serilog;
using Server.Configuration;
using Server.Controllers.Interfaces;
using Server.DTOs.Author;
using Server.ObjectManagers;
using Server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Server.Controllers
{
    internal class AuthorController : IController
    {
        IAuthorService _authorService;
        Serilog.ILogger logger;

        public AuthorController()
        {
            _authorService = SingletonPool.AuthorService;
            logger = SingletonPool.Logger.ForContext<AuthorController>();
        }

        public async void Route(HttpListenerContext ctx)
        {
            try
            {
                var request = ctx.Request;
                var method = request.HttpMethod;
                switch (method)
                {
                    case "GET":
                        RouteGet(ctx);
                        break;
                    case "POST":
                        RoutePost(ctx);
                        break;
                    case "PUT":
                        RoutePut(ctx);
                        break;
                    case "DELETE":
                        RouteDelete(ctx);
                        break;
                }
            }
            catch (Exception ex)
            {
                using (HttpListenerResponse response = ctx.Response)
                {
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }
                logger.Error($"Request routing or unpacking error. Exception: {ex.Message}");
            }
        }

        public async void RouteDelete(HttpListenerContext ctx)
        {
            var request = ctx.Request;
            var path = request.Url.AbsolutePath.Replace(ApiPath.Author, "");
            using (HttpListenerResponse response = ctx.Response)
            {
                if (Regex.IsMatch(path, @"\/\d+"))
                {
                    var param = Convert.ToInt64(path[1..]);
                    await Delete(param, response);
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    logger.Information($"Invalid request url path: {ctx.Request.Url.PathAndQuery}");
                }
            }
        }

        public async void RouteGet(HttpListenerContext ctx)
        {
            var request = ctx.Request;
            var path = request.Url.AbsolutePath.Replace(ApiPath.Author, "");
            using (HttpListenerResponse response = ctx.Response)
            {
                if (Regex.IsMatch(path, @"\/\d+"))
                {
                    var param = Convert.ToInt64(path[1..]);
                    await Get(param, response);
                }
                else if (Regex.IsMatch(path, @""))
                {
                    await Get(response);
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    logger.Information($"Invalid request url path: {ctx.Request.Url.PathAndQuery}");
                }
            }
        }

        public async void RoutePost(HttpListenerContext ctx)
        {
            var request = ctx.Request;
            var path = request.Url.AbsolutePath.Replace(ApiPath.Author, "");
            using (HttpListenerResponse response = ctx.Response)
            {
                if (Regex.IsMatch(path, @""))
                {
                    if (!request.HasEntityBody)
                    {
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return;
                    }
                    string json;
                    using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        json = reader.ReadToEnd();
                    }
                    var body = JsonSerializer.Deserialize<AuthorCreateRequest>(json);
                    await Post(body, response);
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    logger.Information($"Invalid request url path: {ctx.Request.Url.PathAndQuery}");
                }
            }
        }

        public async void RoutePut(HttpListenerContext ctx)
        {
            var request = ctx.Request;
            var path = request.Url.AbsolutePath.Replace(ApiPath.Author, "");
            using (HttpListenerResponse response = ctx.Response)
            {
                if (Regex.IsMatch(path, @"\/\d+"))
                {
                    if (!request.HasEntityBody)
                    {
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return;
                    }
                    string json;
                    using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        json = reader.ReadToEnd();
                    }
                    var param = Convert.ToInt64(path[1..]);
                    var body = JsonSerializer.Deserialize<AuthorUpdateRequest>(json);
                    await Put(param, body, response);
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    logger.Information($"Invalid request url path: {ctx.Request.Url.PathAndQuery}");
                }
            }
        }


        public async Task Get(long id, HttpListenerResponse response)
        {
            try
            {
                var responseObj = await _authorService.GetAuthorById(id);
                if (responseObj == null)
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    logger.Information($"Not Found GetAuthorById with id={id}");
                    response.StatusDescription = $"Author with id={id} not found";
                    return;
                }
                var buffer = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(responseObj));
                response.ContentType = "application/json";
                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.StatusCode = (int)HttpStatusCode.OK;
                logger.Information($"Successful GetAuthorById with id={id}");
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                logger.Error($"Execption during GetAuthorById with id={id}. Exception: {ex.Message}");
            }
        }

        public async Task Get(HttpListenerResponse response)
        {
            try
            {
                var responseObj = await _authorService.GetAllAuthors();
                var buffer = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(responseObj));
                response.ContentType = "application/json";
                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.StatusCode = (int)HttpStatusCode.OK;
                logger.Information($"Successful GetAllAuthors");
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                logger.Error($"Execption during GetAllAuthors. Exception: {ex.Message}");
            }
        }

        public async Task Post(AuthorCreateRequest request, HttpListenerResponse response)
        {
            try
            {
                var responseObj = await _authorService.CreateAuthor(request);
                var buffer = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(responseObj));
                response.ContentType = "application/json";
                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.StatusCode = (int)HttpStatusCode.OK;
                logger.Information($"Successful CreateAuthor");
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                logger.Error($"Execption during CreateAuthor. Exception: {ex.Message}");
            }
        }

        public async Task Put(long id, AuthorUpdateRequest request, HttpListenerResponse response)
        {
            try
            {
                var responseObj = await _authorService.UpdateAuthor(id, request);
                var buffer = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(responseObj));
                response.ContentType = "application/json";
                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.StatusCode = (int)HttpStatusCode.OK;
                logger.Information($"Successful UpdateAuthor");
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                logger.Error($"Execption during UpdateAuthor. Exception: {ex.Message}");
            }
        }

        public async Task Delete(long id, HttpListenerResponse response)
        {
            try
            {
                _authorService.DeleteAuthor(id);
                response.StatusCode = (int)HttpStatusCode.OK;
                logger.Information($"Successful DeleteAuthor with id={id}");
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                logger.Error($"Execption during DeleteAuthor with id={id}. Exception: {ex.Message}");
            }
        }

    }
}
