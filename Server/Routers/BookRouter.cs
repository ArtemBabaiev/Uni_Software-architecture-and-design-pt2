using Serilog;
using Server.Configuration;
using Server.Controllers;
using Server.DTOs.Book;
using Server.Routers.Interfaces;
using Server.Utils;
using System.Net;
using System.Text.RegularExpressions;

namespace Server.Routers
{
    internal class BookRouter : IRouter
    {
        private readonly string RouterPath = ApiPath.Book;
        public ILogger Logger { get; protected set; }
        private BookController _bookController;

        public BookRouter()
        {
            Logger = Log.Logger.ForContext<BookRouter>();
            _bookController = new BookController();
        }

        public async void RouteDelete(HttpListenerContext ctx)
        {
            var reqData = HttpHelper.UnpackRequest(ctx.Request, RouterPath);


            if (Regex.IsMatch(reqData.NoApiPath, ApiPath.IdRegex))
            {
                var param = Convert.ToInt64(reqData.NoApiPath[1..]);
                var actionResponse = await _bookController.Delete(param);
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
                var actionRespose = await _bookController.Get(param);
                HttpHelper.PackResponse(ctx, actionRespose);
            }
            else if (Regex.IsMatch(reqData.NoApiPath, ApiPath.BlankRegex))
            {
                var actionRespose = await _bookController.Get();
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

                var actionResponse = await _bookController.Post(reqData.GetBodyObject<CreateBookRequest>());
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
                var actionResponse = await _bookController.Put(param, reqData.GetBodyObject<UpdateBookRequest>());
                HttpHelper.PackResponse(ctx, actionResponse);
            }
            else
            {
                HttpHelper.PackResponse(ctx, ActionHelper.NotFound());
            }
        }
    }
}
