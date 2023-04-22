﻿using Server.Models;
using Server.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Repositories
{
    internal class ExemplarRepository : GenericRepository<Exemplar>, IExemplarRepository
    {
        public ExemplarRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction) : base(sqlConnection, dbTransaction, "Exemplars")
        {
        }
    }
}
