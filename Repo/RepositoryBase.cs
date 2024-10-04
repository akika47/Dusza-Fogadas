using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Dusza.Repo
{
    public abstract class RepositoryBase
    {
        readonly string _connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=betting";

        protected MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
