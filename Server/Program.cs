using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Xml.Schema;
using Server.ObjectManagers;
using AutoMapper;
using Server.Configuration;
using Server.Services;
using Server.Controllers.Interfaces;
using Serilog;

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
            SingletonPool.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                //.WriteTo.File("log.txt")
                .CreateLogger();

            SingletonPool.AuthorService = new AuthorService();

        }
    }
}