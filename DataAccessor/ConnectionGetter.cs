using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace DataAccessor
{
    class ConnectionGetter
    {
        private static string db_str= new StreamReader("dbconfig.cof").ReadLine();
        public static IDbConnection GetConnection()
        {
            return new MySqlConnection(db_str);
        }
    }
}
