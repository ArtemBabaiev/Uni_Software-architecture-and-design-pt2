﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.Data.Models
{
    internal class Exemplar
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsLend { get; set; }
        public long BookId { get; set; }

        public string? BookName { get; set; }
    }
}
