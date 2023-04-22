using System.ComponentModel;

namespace Server.Models
{
    internal class Exemplar
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsLend { get; set; }
        public long BookId { get; set; }

        [DescriptionAttribute("ignore")]
        public Book? Book { get; set; }
    }
}
