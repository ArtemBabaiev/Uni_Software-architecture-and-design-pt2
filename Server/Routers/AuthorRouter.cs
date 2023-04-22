using Serilog;
using Server.Configuration;
using Server.Controllers;
using Server.DTOs.Author;
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
            Logger = Log.Logger.ForContext<AuthorRouter>();
            _authorController = new AuthorController();
        }

        public async void RouteDelete(HttpListenerContext ctx)
        {
            var reqData = HttpHelper.UnpackRequest(ctx.Request, RouterPath);


            if (Regex.IsMatch(reqData.NoApiPath, ApiPath.IdRegex))
            {
                var param = Convert.ToInt64(reqData.NoApiPath[1..]);
                var actionResponse = await _authorController.Delete(param);
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
                var actionRespose = await _authorController.Get(param);
                HttpHelper.PackResponse(ctx, actionRespose);
            }
            else if (Regex.IsMatch(reqData.NoApiPath, ApiPath.BlankRegex))
            {
                var actionRespose = await _authorController.Get();
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

                var actionResponse = await _authorController.Post(reqData.GetBodyObject<CreateAuthorRequest>());
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
                var actionResponse = await _authorController.Put(param, reqData.GetBodyObject<UpdateAuthorRequest>());
                HttpHelper.PackResponse(ctx, actionResponse);
            }
            else
            {
                HttpHelper.PackResponse(ctx, ActionHelper.NotFound());
            }
        }
    }
}
