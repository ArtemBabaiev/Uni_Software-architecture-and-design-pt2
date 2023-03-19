using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    internal abstract class BaseModel
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
