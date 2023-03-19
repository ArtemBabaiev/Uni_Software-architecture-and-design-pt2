using AutoMapper;
using Server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.ObjectManagers
{
    internal static class SingletonPool
    {
        public static string ConnectionString { get; set; }
        public static MapperConfiguration MapperConfiguration { get; set; }
        public static Serilog.ILogger Logger { get; set; }
        public static IAuthorService AuthorService { get; set; }
    }
}
