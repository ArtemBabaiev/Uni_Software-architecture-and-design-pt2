using Serilog;
using Server.Configuration;
using Server.Controllers;
using Server.DTOs;
using Server.DTOs.Author;
using Server.ObjectManagers;
using Server.Routers.Interfaces;
using Server.Utils;
using System.Net;
using System.Text.RegularExpressions;

namespace Server.Routers
{
    internal class AuthorRouter : IRouter
    {
        private readonly string RouterPath = ApiPath.Author;
        public ILogger Logger { get; protected set; }
        private AuthorController _authorController;

        public AuthorRouter()
        {
            Logger = SingletonPool.Logger.ForContext<AuthorRouter>();
            _authorController = new AuthorController();
        }

        public async void RouteDelete(HttpListenerContext ctx)
        {
            var reqData = RouterHelper.UnpackRequest(ctx.Request, RouterPath);

            using (HttpListenerResponse response = ctx.Response)
            {
                if (Regex.IsMatch(reqData.NoApiPath, @"\/\d+"))
                {
                    var param = Convert.ToInt64(reqData.NoApiPath[1..]);
                    var actionResponse = await _authorController.Delete(param);
                    RouterHelper.PackResponse(response, actionResponse);
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    Logger.Information($"Invalid request url path: {ctx.Request.Url.PathAndQuery}");
                }
            }
        }

        public async void RouteGet(HttpListenerContext ctx)
        {
            var reqData = RouterHelper.UnpackRequest(ctx.Request, RouterPath);
            using (HttpListenerResponse response = ctx.Response)
            {
                if (Regex.IsMatch(reqData.NoApiPath, @"\/\d+"))
                {
                    var param = Convert.ToInt64(reqData.NoApiPath[1..]);
                    var actionRespose = await _authorController.Get(param);
                    RouterHelper.PackResponse(response, actionRespose);
                }
                else if (Regex.IsMatch(reqData.NoApiPath, @""))
                {
                    var actionRespose = await _authorController.Get();
                    RouterHelper.PackResponse(response, actionRespose);
                }
                else
                {
                    RouterHelper.PackResponse(response, new ActionResponse(HttpStatusCode.NotFound));
                    Logger.Information($"Invalid request url path: {ctx.Request.Url.PathAndQuery}");
                }
            }
        }

        public async void RoutePost(HttpListenerContext ctx)
        {
            var reqData = RouterHelper.UnpackRequest(ctx.Request, RouterPath);

            using (HttpListenerResponse response = ctx.Response)
            {
                if (Regex.IsMatch(reqData.NoApiPath, @""))
                {
                    if (reqData.Json == null)
                    {
                        RouterHelper.PackResponse(response, ResponseHelper.BadRequest());
                        return;
                    }

                    var actionResponse = await _authorController.Post(reqData.GetBodyObject<AuthorCreateRequest>());
                    RouterHelper.PackResponse(response, actionResponse);
                }
                else
                {
                    RouterHelper.PackResponse(response, ResponseHelper.NotFound());
                    Logger.Information($"Invalid request url path: {ctx.Request.Url.PathAndQuery}");
                }
            }
        }

        public async void RoutePut(HttpListenerContext ctx)
        {
            var reqData = RouterHelper.UnpackRequest(ctx.Request, RouterPath);
            using (HttpListenerResponse response = ctx.Response)
            {
                if (Regex.IsMatch(reqData.NoApiPath, @"\/\d+"))
                {
                    if (reqData.Json == null)
                    {
                        RouterHelper.PackResponse(response, ResponseHelper.BadRequest());
                        return;
                    }

                    var param = Convert.ToInt64(reqData.NoApiPath[1..]);
                    var actionResponse = await _authorController.Put(param, reqData.GetBodyObject<AuthorUpdateRequest>());
                    RouterHelper.PackResponse(response, actionResponse);
                }
                else
                {
                    RouterHelper.PackResponse(response, ResponseHelper.NotFound());
                    Logger.Information($"Invalid request url path: {ctx.Request.Url.PathAndQuery}");
                }
            }
        }


    }
}
