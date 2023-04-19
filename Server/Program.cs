using AutoMapper;
using Serilog;
using Server.Configuration;
using Server.ObjectManagers;
using Server.Services;
using System.Collections.Specialized;
using System.Configuration;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NameValueCollection sAll = ConfigurationManager.AppSettings;
            InitPool(sAll);
            string host = sAll.Get("Host");
            Listener listener = new Listener(host);
            listener.StartListening();
        }

        static void InitPool(NameValueCollection sAll)
        {
            SingletonPool.ConnectionString = sAll.Get("ConnectionString");
            SingletonPool.MapperConfiguration = new MapperConfiguration(cfg =>
                cfg.AddProfile<MapperProfile>()
            );
            var mt = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Properties:j} {Message:lj}{NewLine}{Exception}";
            SingletonPool.Logger = new LoggerConfiguration()
                .WriteTo.Console(outputTemplate: mt)
                .WriteTo.File("log.txt", outputTemplate: mt)
                .CreateLogger();

            SingletonPool.AuthorService = new AuthorService();

        }
    }
}