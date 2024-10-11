using MySql.Data.MySqlClient;
namespace WPF_Dusza.Repo
{

    public abstract class RepositoryBase
    {
        protected string cmd = "";
        readonly string _connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=betting";

        protected MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
