using System.Text.Json;

namespace Server.Network
{
    internal class RequestData
    {
        public string AbsolutePath { get; set; }
        public string NoApiPath { get; set; }
        public Dictionary<string, string> QueryParams { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public string? Json { get; set; }
        public T? GetBodyObject<T>() where T : class
        {
            return JsonSerializer.Deserialize<T>(Json);
        }
    }
}
