using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestHoovers.Services
{
    public class SqlService
    {
        public static SqlConnection GetSqlConnection()
        {
            //Data Source=DESKTOP-TCHCACV;Initial Catalog=ClienteIX; Integrated Security=True; Pooling=False
            return new SqlConnection(@"Data Source=.;Initial Catalog=HOOVERS; Integrated Security=True; Pooling=False");
        }
    }
}
