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
            Logger = Log.Logger.ForContext<AuthorRouter>();
            _authorController = new AuthorController();
        }

        public async void RouteDelete(HttpListenerContext ctx)
        {
            var reqData = ResponseHelper.UnpackRequest(ctx.Request, RouterPath);


            if (Regex.IsMatch(reqData.NoApiPath, ApiPath.IdRegex))
            {
                var param = Convert.ToInt64(reqData.NoApiPath[1..]);
                var actionResponse = await _authorController.Delete(param);
                ResponseHelper.PackResponse(ctx, actionResponse);
            }
            else
            {
                ResponseHelper.PackResponse(ctx, ActionHelper.NotFound());
            }
        }

        public async void RouteGet(HttpListenerContext ctx)
        {
            var reqData = ResponseHelper.UnpackRequest(ctx.Request, RouterPath);
            if (Regex.IsMatch(reqData.NoApiPath, ApiPath.IdRegex))
            {
                var param = Convert.ToInt64(reqData.NoApiPath[1..]);
                var actionRespose = await _authorController.Get(param);
                ResponseHelper.PackResponse(ctx, actionRespose);
            }
            else if (Regex.IsMatch(reqData.NoApiPath, ApiPath.BlankRegex))
            {
                var actionRespose = await _authorController.Get();
                ResponseHelper.PackResponse(ctx, actionRespose);
            }
            else
            {
                ResponseHelper.PackResponse(ctx, ActionHelper.NotFound());
            }
        }

        public async void RoutePost(HttpListenerContext ctx)
        {
            var reqData = ResponseHelper.UnpackRequest(ctx.Request, RouterPath);

            if (Regex.IsMatch(reqData.NoApiPath, ApiPath.BlankRegex))
            {
                if (reqData.Json == null)
                {
                    ResponseHelper.PackResponse(ctx, ActionHelper.BadRequest());
                    return;
                }

                var actionResponse = await _authorController.Post(reqData.GetBodyObject<CreateAuthorRequest>());
                ResponseHelper.PackResponse(ctx, actionResponse);
            }
            else
            {
                ResponseHelper.PackResponse(ctx, ActionHelper.NotFound());
            }
        }

        public async void RoutePut(HttpListenerContext ctx)
        {
            var reqData = ResponseHelper.UnpackRequest(ctx.Request, RouterPath);
            if (Regex.IsMatch(reqData.NoApiPath, ApiPath.IdRegex))
            {
                if (reqData.Json == null)
                {
                    ResponseHelper.PackResponse(ctx, ActionHelper.BadRequest());
                    return;
                }

                var param = Convert.ToInt64(reqData.NoApiPath[1..]);
                var actionResponse = await _authorController.Put(param, reqData.GetBodyObject<UpdateAuthorRequest>());
                ResponseHelper.PackResponse(ctx, actionResponse);
            }
            else
            {
                ResponseHelper.PackResponse(ctx, ActionHelper.NotFound());
            }
        }


    }
}
