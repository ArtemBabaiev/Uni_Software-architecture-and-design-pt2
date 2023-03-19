using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.ObjectManagers
{
    internal static class ObjectPool
    {
        public static string ConnectionString { get; set; }
        public static MapperConfiguration MapperConfiguration { get; set; }
    }
}
