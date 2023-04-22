namespace Server.DTOs.Exemplar
{
    internal class CreateExemplarRequest
    {
        public bool IsLend { get; set; }
        public long BookId { get; set; }
    }
}
