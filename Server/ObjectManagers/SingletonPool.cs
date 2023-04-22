using AutoMapper;

namespace Server.ObjectManagers
{
    internal static class SingletonPool
    {
        public static string ConnectionString { get; set; }
        public static MapperConfiguration MapperConfiguration { get; set; }
    }
}
