using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Configuration
{
    internal static class ApiPath
    {
        public static readonly string Author = "/api/authors";
        public static readonly string AuthorRegex = @"\/api\/authors.*";
    }
}
