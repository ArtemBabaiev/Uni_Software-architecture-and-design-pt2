namespace Server.DTOs.Book
{
    internal class GetBookResponse
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
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
