using Serilog;
using Server.Configuration;
using Server.Controllers;
using Server.DTOs.Publisher;
using Server.Routers.Interfaces;
using Server.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Server.Routers
{
    internal class PublisherRouter : IRouter
    {
        private readonly string RouterPath = ApiPath.Publisher;
        public ILogger Logger { get; protected set; }
        private PublisherController _publisherController;

        public PublisherRouter()
        {
            Logger = Log.Logger.ForContext<PublisherRouter>();
            _publisherController = new PublisherController();
        }

        public async void RouteDelete(HttpListenerContext ctx)
        {
            var reqData = HttpHelper.UnpackRequest(ctx.Request, RouterPath);


            if (Regex.IsMatch(reqData.NoApiPath, ApiPath.IdRegex))
            {
                var param = Convert.ToInt64(reqData.NoApiPath[1..]);
                var actionResponse = await _publisherController.Delete(param);
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
                var actionRespose = await _publisherController.Get(param);
                HttpHelper.PackResponse(ctx, actionRespose);
            }
            else if (Regex.IsMatch(reqData.NoApiPath, ApiPath.BlankRegex))
            {
                var actionRespose = await _publisherController.Get();
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

                var actionResponse = await _publisherController.Post(reqData.GetBodyObject<CreatePublisherRequest>());
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
                var actionResponse = await _publisherController.Put(param, reqData.GetBodyObject<UpdatePublisherRequest>());
                HttpHelper.PackResponse(ctx, actionResponse);
            }
            else
            {
                HttpHelper.PackResponse(ctx, ActionHelper.NotFound());
            }
        }
    }
}
