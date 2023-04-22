namespace Server.DTOs.Book
{
    internal class CreateBookRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Isbn { get; set; }
        public int NumberOfPages { get; set; }
        public int PublishingYear { get; set; }
        public long AuthorId { get; set; }
        public long PublisherId { get; set; }
        public long GenreId { get; set; }
    }
}
