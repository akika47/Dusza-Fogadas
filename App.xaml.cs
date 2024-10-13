using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto;
using System.IO;
using System.Windows;
using System.Windows.Markup;
using WPF_Dusza.Utils;


namespace WPF_Dusza
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        const string _connstring = "datasource=127.0.0.1;port=3306;username=root;password=;database=";
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);  
            await StartAppAsync();
        }
        async Task StartAppAsync()
        {
            try
            {
                using MySqlConnection connection = new(_connstring);
                StreamReader reader = null;
                MySqlCommand command = new("", connection);
                await connection.OpenAsync();
                bool DBExsits = await DatabaseExists(connection);
                if (!DBExsits)
                {
                    reader = new(@".\Database\Database_Mysql.txt");
                    string cmd = await reader.ReadToEndAsync();
                    command = new(cmd, connection);
                    await command.ExecuteNonQueryAsync();
                }
                await LoadDataToDB(reader, command);
            }
            catch (Exception ex) 
            {
                WindowUtils.DisplayErrorMessage("Hiba! Nem sikerült elérni az adatbázist!\n " +
                    $"Az alkalmazás most leáll (Hiba:{ex.Message} )");

                Current.Shutdown();
            }
        }
        async Task LoadDataToDB(StreamReader reader, MySqlCommand command)
        {
            reader = new(@".\Database\data.txt");
            command.CommandText = await reader.ReadToEndAsync();
            await command.ExecuteNonQueryAsync();
        }
        async Task<bool> DatabaseExists(MySqlConnection connection)
        {
            string cmd = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME=betting"; 
            using MySqlCommand command = new(cmd,connection);
            int exists = Convert.ToInt32(await command.ExecuteScalarAsync());
            return exists > 0;
        }
    }

}
