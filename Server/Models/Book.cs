using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Server.Models
{
    internal class Book : BaseModel
    {
        public string? Isbn { get; set; }
        public int NumberOfPages { get; set; }
        public int PublishingYear { get; set; }
        public long AuthorId { get; set; }
        public long PublisherId { get; set; }
        public long GenreId { get; set; }

        [DescriptionAttribute("ignore")]
        public string? AuthorName { get; set; }
        [DescriptionAttribute("ignore")]
        public string? PublisherName { get; set; }
        [DescriptionAttribute("ignore")]
        public string? GenreName { get; set; }

    }
}
