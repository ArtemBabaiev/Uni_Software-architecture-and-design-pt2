using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.Data.Models
{
    internal class Book : BaseModel
    {
        public string? Isbn { get; set; }
        public int NumberOfPages { get; set; }
        public int PublishingYear { get; set; }
        public long AuthorId { get; set; }
        public long PublisherId { get; set; }
        public long GenreId { get; set; }

        public string? AuthorName { get; set; }
        public string? PublisherName { get; set; }
        public string? GenreName { get; set; }
    }
}
