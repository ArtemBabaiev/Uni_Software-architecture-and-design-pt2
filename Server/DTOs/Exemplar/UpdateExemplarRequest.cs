using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DTOs.Exemplar
{
    internal class UpdateExemplarRequest
    {
        public bool IsLend { get; set; }
        public long BookId { get; set; }
    }
}
