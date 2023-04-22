using Server.Utils;
using System.Net;

namespace Server.Routers.Interfaces
{
    internal interface IRouter
    {
        Serilog.ILogger Logger { get; }
        void Route(HttpListenerContext ctx)
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
                Logger.Error(ex, "Somethig went wrong during routing process");
                HttpHelper.PackResponse(ctx, ActionHelper.InternalServerError(ex.Message));
            }
        }
        void RouteGet(HttpListenerContext ctx);
        void RoutePost(HttpListenerContext ctx);
        void RoutePut(HttpListenerContext ctx);
        void RouteDelete(HttpListenerContext ctx);
    }
}
