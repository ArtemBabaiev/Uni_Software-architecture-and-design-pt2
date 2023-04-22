using Serilog;
using Server.Configuration;
using Server.ObjectManagers;
using Server.Routers;
using Server.Routers.Interfaces;
using Server.Utils;
using System.Net;
using System.Text.RegularExpressions;

namespace Server
{
    internal class Listener
    {
        HttpListener listener;
        IRouter authorRouter;
        IRouter bookRouter;
        IRouter exemplarRouter;
        IRouter genreRouter;
        IRouter publisherRouter;
        ILogger logger;
        private int numberOfEntries;

        public Listener(string prefix, int toAccept = 2)
        {
            this.listener = new HttpListener();
            listener.Prefixes.Add(prefix);
            logger = Log.Logger.ForContext<Listener>();
            this.numberOfEntries = Environment.ProcessorCount * toAccept;
            authorRouter = new AuthorRouter();
            bookRouter = new BookRouter();
            exemplarRouter = new ExemplarRouter();
            genreRouter = new GenreRouter();
            publisherRouter = new PublisherRouter();
        }

        public async void StartListening()
        {
            listener.Start();

            var sem = new Semaphore(numberOfEntries, numberOfEntries);

            logger.Information("Listening on " + String.Join("; ", listener.Prefixes));

            while (true)
            {
                /*HttpListenerContext ctx = listener.GetContext();
                logger.Information($"Incoming request: {ctx.Request.Url.PathAndQuery}");
                Task.Factory.StartNew(() => ProcessRequest(ctx));*/

                sem.WaitOne();
                listener.GetContextAsync().ContinueWith(async (t) =>
                {
                    sem.Release();
                    var ctx = await t;
                    ProcessRequest(ctx);
                });
            }
        }

        public void ProcessRequest(HttpListenerContext context)
        {
            Log.Logger.Information($"Request starting HTTP/{context.Request.ProtocolVersion} {context.Request.HttpMethod} {context.Request.Url.PathAndQuery} - - ");

            var path = context.Request.Url.AbsolutePath;

            if (Regex.IsMatch(path, ApiPath.AuthorRegex))
            {
                authorRouter.Route(context);
            }
            else if (Regex.IsMatch(path, ApiPath.BookRegex))
            {
                bookRouter.Route(context);
            }
            else if (Regex.IsMatch(path, ApiPath.ExemplarRegex))
            {
                exemplarRouter.Route(context);
            }
            else if (Regex.IsMatch(path, ApiPath.GenreRegex))
            {
                genreRouter.Route(context);
            }
            else if (Regex.IsMatch(path, ApiPath.PublisherRegex))
            {
                publisherRouter.Route(context);
            }
            else
            {
                HttpHelper.PackResponse(context, ActionHelper.NotFound());
            }
        }
    }
}
