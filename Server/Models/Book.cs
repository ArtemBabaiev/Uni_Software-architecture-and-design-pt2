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
        public Author? Author { get; set; }
        [DescriptionAttribute("ignore")]
        public Publisher? Publisher { get; set; }
        [DescriptionAttribute("ignore")]
        public Genre? Genre { get; set; }

    }
}
