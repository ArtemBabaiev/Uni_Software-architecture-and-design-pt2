namespace Server.DTOs.Author
{
    internal class AuthorResponse
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime LastUpdatedAt { get; set; }

    }
}
