using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Server.Controllers.Interfaces
{
    internal interface IController
    {
        void Route(HttpListenerContext ctx);
        void RouteGet(HttpListenerContext ctx);
        void RoutePost(HttpListenerContext ctx);
        void RoutePut(HttpListenerContext ctx);
        void RouteDelete(HttpListenerContext ctx);
    }
}
