using Server.Configuration;
using Server.ObjectManagers;
using Server.Routers;
using Server.Routers.Interfaces;
using System.Net;
using System.Text.RegularExpressions;

namespace Server
{
    internal class Listener
    {
        HttpListener listener;
        IRouter authorRouter;
        Serilog.ILogger logger;
        private int numberOfEntries;

        public Listener(string prefix, int toAccept = 2)
        {
            this.listener = new HttpListener();
            listener.Prefixes.Add(prefix);
            authorRouter = new AuthorRouter();
            logger = SingletonPool.Logger.ForContext<Listener>();
            this.numberOfEntries = Environment.ProcessorCount * toAccept;
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
                    logger.Information($"Incoming request: {ctx.Request.Url.PathAndQuery}");
                    ProcessRequest(ctx);
                });
            }
        }

        public void ProcessRequest(HttpListenerContext context)
        {
            var url = context.Request.Url;
            var path = url.AbsolutePath;
            if (Regex.IsMatch(path, ApiPath.AuthorRegex))
            {
                authorRouter.Route(context);
            }
            else
            {
                logger.Information($"Invalid request url path: {context.Request.Url.PathAndQuery}");
            }
        }
    }
}
