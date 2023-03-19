using Server.Configuration;
using Server.Controllers;
using Server.ObjectManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Server
{
    internal class Listener
    {
        HttpListener listener;
        AuthorController authorController;
        Serilog.Core.Logger logger;

        public Listener(string prefix)
        {
            this.listener = new HttpListener();
            listener.Prefixes.Add(prefix);
            authorController = new AuthorController();
            logger = SingletonPool.Logger;
        }

        public async void StartListening()
        {
            listener.Start();

            logger.Information("Listening on port 13000...");

            while (true)
            {
                HttpListenerContext ctx = listener.GetContext();
                logger.Information($"Incoming request: {ctx.Request.Url.PathAndQuery}");
                Task.Factory.StartNew(() => ProcessRequest(ctx));
            }
        }
        
        public void ProcessRequest(HttpListenerContext context)
        {
            var url = context.Request.Url;
            var path = url.AbsolutePath;
            if (Regex.IsMatch(path, ApiPath.AuthorRegex))
            {
                authorController.Route(context);
            }
            else
            {
                logger.Information($"Invalid request url path: {context.Request.Url.PathAndQuery}");
            }
        }
    }
}
