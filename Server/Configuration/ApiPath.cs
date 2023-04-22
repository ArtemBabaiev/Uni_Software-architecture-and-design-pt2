namespace Server.Configuration
{
    internal static class ApiPath
    {
        private static readonly string api = "api";

        private static readonly string authors = "authors";
        public static readonly string Author = $"/{api}/{authors}";
        public static readonly string AuthorRegex = @$"\/{api}\/{authors}(\/.*)?";

        private static readonly string books = "books";
        public static readonly string Book = $"/{api}/{books}";
        public static readonly string BookRegex = @$"\/{api}\/{books}(\/.*)?";

        private static readonly string exemplars = "exemplars";
        public static readonly string Exemplar = $"/{api}/{exemplars}";
        public static readonly string ExemplarRegex = @$"\/{api}\/{exemplars}(\/.*)?";

        private static readonly string genres = "genres";
        public static readonly string Genre = $"/{api}/{genres}";
        public static readonly string GenreRegex = @$"\/{api}\/{genres}(\/.*)?";

        private static readonly string publishers = "publishers";
        public static readonly string Publisher = $"/{api}/{publishers}";
        public static readonly string PublisherRegex = @$"\/{api}\/{publishers}(\/.*)?";

        public static readonly string IdRegex = @"\/\d+";
        public static readonly string BlankRegex = @"^$";
    }
}
