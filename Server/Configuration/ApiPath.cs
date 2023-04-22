namespace Server.Configuration
{
    internal static class ApiPath
    {
        public static readonly string Author = "/api/authors";
        public static readonly string AuthorRegex = @"\/api\/authors.*";


        public static readonly string IdRegex = @"\/\d+";
        public static readonly string BlankRegex = @"";
    }
}
