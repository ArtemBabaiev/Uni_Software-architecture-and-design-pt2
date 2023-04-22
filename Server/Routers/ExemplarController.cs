using Serilog;
using Server.Configuration;
using Server.Controllers;
using Server.DTOs.Exemplar;
using Server.Routers.Interfaces;
using Server.Utils;
using System.Net;
using System.Text.RegularExpressions;

namespace Server.Routers
{
    internal class ExemplarRouter : IRouter
    {
        private readonly string RouterPath = ApiPath.Exemplar;
        public ILogger Logger { get; protected set; }
        private ExemplarController _exemplarController;

        public ExemplarRouter()
        {
            Logger = Log.Logger.ForContext<ExemplarRouter>();
            _exemplarController = new ExemplarController();
        }

        public async void RouteDelete(HttpListenerContext ctx)
        {
            var reqData = HttpHelper.UnpackRequest(ctx.Request, RouterPath);


            if (Regex.IsMatch(reqData.NoApiPath, ApiPath.IdRegex))
            {
                var param = Convert.ToInt64(reqData.NoApiPath[1..]);
                var actionResponse = await _exemplarController.Delete(param);
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
                var actionRespose = await _exemplarController.Get(param);
                HttpHelper.PackResponse(ctx, actionRespose);
            }
            else if (Regex.IsMatch(reqData.NoApiPath, ApiPath.BlankRegex))
            {
                var actionRespose = await _exemplarController.Get();
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

                var actionResponse = await _exemplarController.Post(reqData.GetBodyObject<CreateExemplarRequest>());
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
                var actionResponse = await _exemplarController.Put(param, reqData.GetBodyObject<UpdateExemplarRequest>());
                HttpHelper.PackResponse(ctx, actionResponse);
            }
            else
            {
                HttpHelper.PackResponse(ctx, ActionHelper.NotFound());
            }
        }
    }
}
