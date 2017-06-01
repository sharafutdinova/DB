using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;

namespace DB2.Middleware
{
    public class NpgsqlHelper
    {
        const string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=123;Database=DB;SSL Mode=Prefer";

        public static NpgsqlConnection Connection { get; private set; }
        static NpgsqlHelper()
        {
            Connection = new NpgsqlConnection(connectionString);
        }

    }
}