using Serilog;
using Server.Configuration;
using Server.Controllers;
using Server.DTOs.Genre;
using Server.Routers.Interfaces;
using Server.Utils;
using System.Net;
using System.Text.RegularExpressions;

namespace Server.Routers
{
    internal class GenreRouter : IRouter
    {
        private readonly string RouterPath = ApiPath.Genre;
        public ILogger Logger { get; protected set; }
        private GenreController _genreController;

        public GenreRouter()
        {
            Logger = Log.Logger.ForContext<GenreRouter>();
            _genreController = new GenreController();
        }

        public async void RouteDelete(HttpListenerContext ctx)
        {
            var reqData = HttpHelper.UnpackRequest(ctx.Request, RouterPath);


            if (Regex.IsMatch(reqData.NoApiPath, ApiPath.IdRegex))
            {
                var param = Convert.ToInt64(reqData.NoApiPath[1..]);
                var actionResponse = await _genreController.Delete(param);
                HttpHelper.PackResponse(ctx, actionResponse);
            }
            else
            {
                HttpHelper.PackResponse(ctx, ActionHelper.NotFound());
            }
        }

        public async void RouteGet(HttpListenerContext ctx)
        {
            var reqData = HttpHelper.UnpackRequest(ctx.Request, RouterPath);
            if (Regex.IsMatch(reqData.NoApiPath, ApiPath.IdRegex))
            {
                var param = Convert.ToInt64(reqData.NoApiPath[1..]);
                var actionRespose = await _genreController.Get(param);
                HttpHelper.PackResponse(ctx, actionRespose);
            }
            else if (Regex.IsMatch(reqData.NoApiPath, ApiPath.BlankRegex))
            {
                var actionRespose = await _genreController.Get();
                HttpHelper.PackResponse(ctx, actionRespose);
            }
            else
            {
                HttpHelper.PackResponse(ctx, ActionHelper.NotFound());
            }
        }

        public async void RoutePost(HttpListenerContext ctx)
        {
            var reqData = HttpHelper.UnpackRequest(ctx.Request, RouterPath);

            if (Regex.IsMatch(reqData.NoApiPath, ApiPath.BlankRegex))
            {
                if (reqData.Json == null)
                {
                    HttpHelper.PackResponse(ctx, ActionHelper.BadRequest());
                    return;
                }

                var actionResponse = await _genreController.Post(reqData.GetBodyObject<CreateGenreRequest>());
                HttpHelper.PackResponse(ctx, actionResponse);
            }
            else
            {
                HttpHelper.PackResponse(ctx, ActionHelper.NotFound());
            }
        }

        public async void RoutePut(HttpListenerContext ctx)
        {
            var reqData = HttpHelper.UnpackRequest(ctx.Request, RouterPath);
            if (Regex.IsMatch(reqData.NoApiPath, ApiPath.IdRegex))
            {
                if (reqData.Json == null)
                {
                    HttpHelper.PackResponse(ctx, ActionHelper.BadRequest());
                    return;
                }

                var param = Convert.ToInt64(reqData.NoApiPath[1..]);
                var actionResponse = await _genreController.Put(param, reqData.GetBodyObject<UpdateGenreRequest>());
                HttpHelper.PackResponse(ctx, actionResponse);
            }
            else
            {
                HttpHelper.PackResponse(ctx, ActionHelper.NotFound());
            }
        }
    }
}
