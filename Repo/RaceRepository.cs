using MySql.Data.MySqlClient;
using WPF_Dusza.Models;

namespace WPF_Dusza.Repo
{
    public sealed class RaceRepository : RepositoryBase
    {
            // LINQ-s lekérdezések

        public IEnumerable<User> GetAllUsers()
        {
            string commandstr = "SELECT * FROM users";
            using MySqlConnection connection = GetConnection();
            using MySqlCommand command = new(commandstr, connection);
            connection.Open();
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) 
            {
                yield return new User 
                { 
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Password = reader.GetString(2),
                    Role = reader.GetInt32(3)
                };
            }
        }
        public async Task RegisterUserAsync(User user) 
        {
            string cmd = "INSERT into users(name,password,points,role) VALUES(@username,@password,@points,@role)";
            using MySqlConnection connection = GetConnection();
            using MySqlCommand command = new(cmd, connection);
            await connection.OpenAsync();
            command.Parameters.AddWithValue("@username", user.Name);
            command.Parameters.AddWithValue("@password", user.Password);
            command.Parameters.AddWithValue("@points", 100);
            command.Parameters.AddWithValue("@role", user.Role);
            await command.ExecuteNonQueryAsync();
        }
        public async Task ModifyUserAsync(User user)
        {
            string cmd = "UPDATE TABLE users SET name=@name, password=@password, points=@points role=@role"
                + $"WHERE id={user.Id}";
            using MySqlConnection connection = GetConnection();
            using MySqlCommand command = new(cmd, connection);
            await connection.OpenAsync();
            command.Parameters.AddWithValue("@name", user.Name);
            command.Parameters.AddWithValue("@password", user.Password);
            command.Parameters.AddWithValue("@points", user.Points);
            command.Parameters.AddWithValue("@role", user.Role);

        }
        public IEnumerable<Game> GetGames()
        {
            string cmd = "SELECT g.id as game_id  g.name AS game_name, u.name AS organizer_name,  p.name AS participant_name, g.status AS game_status" +
                "FROM  games g  JOIN users u ON g.userId = u.id JOIN participants p ON g.id = p.gameId ORDER BY g.name;";
            using MySqlConnection conn = GetConnection();
            using MySqlCommand command = new(cmd, conn);
            conn.Open();
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) 
            {
                yield return new Game
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    OrganizerName = reader.GetString(2),
                    FirstParticipant = reader.GetString(3),
                    SecondParticipant = reader.GetString(4),
                    IsGameOver = reader.GetBoolean(5),
                };
            }
        }
        public async Task PlaceBetAsync(Bet bet)
        {
            string cmd = "INSERT INTO  bets(eventId,userId,participantId,prediction,betAmount) " +
                " VALUES(@eventId,@userId, @participantId, @prediciton, @betAmount)";
            using MySqlConnection conn = GetConnection();
            using MySqlCommand command = new(cmd, conn);
            await conn.OpenAsync();
            //TODO replace with the actual values
            command.Parameters.AddWithValue("@eventId", bet.EventId);
            command.Parameters.AddWithValue("@userId", bet.UserID);
            command.Parameters.AddWithValue("@participantId", bet.ParticipantId);
            command.Parameters.AddWithValue("@prediction", bet.Prediction);
            command.Parameters.AddWithValue("@betAmount", bet.BetAmount);
            await command.ExecuteNonQueryAsync();
        }
        public IEnumerable<string> GetEvents(Game game)
        {
            string cmd = $"SELECT eventName FROM events WHERE gameId={game.Id}";
            using MySqlConnection conn = GetConnection();
            using MySqlCommand command = new(cmd, conn);
            conn.Open();
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) 
            {
                yield return $"{reader.GetString(0)}";
            }

        }
        public async Task CreateNewGameAsync(User user)
        {
            string cmd = "INSERT INTO games(name, userId, status) VALUES(@name,@userId,@status) ";
            using MySqlConnection conn = GetConnection();
            using MySqlCommand command = new(cmd, conn);
            await conn.OpenAsync();
            command.Parameters.AddWithValue("@name", "");
            command.Parameters.AddWithValue("@userId", user.Id);
            command.Parameters.AddWithValue("@status", 0);
            await command.ExecuteNonQueryAsync();
        }
        public async Task CloseGameAsync(Game game)
        {
            throw new NotImplementedException();
        }
    }
}
