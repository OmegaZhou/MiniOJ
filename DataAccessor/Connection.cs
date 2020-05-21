using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace DataAccessor
{
    class Connection
    {
        static string db_str= new StreamReader("dbconfig.cof").ReadLine();
        static IDbConnection GetConnection()
        {
            return new MySqlConnection(db_str);
        }
    }
}
