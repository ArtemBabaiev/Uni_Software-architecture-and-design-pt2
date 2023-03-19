using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DTOs.Author
{
    internal class AuthorUpdateRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
