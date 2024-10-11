using MySql.Data.MySqlClient;
using WPF_Dusza.Models;
using WPF_Dusza.Models;

namespace WPF_Dusza.Repo
{
    public sealed class BettingRepository
    {
        UserRepo _userRepo = new();
        GameRepo _gameRepo = new();
        BetRepo _betRepo = new();
        EventRepo _eventRepo = new();
        public UserRepo UserRepository { get => _userRepo; }
        public GameRepo GameRepository { get => _gameRepo; }
        public BetRepo BetRepository { get => _betRepo; }
        public EventRepo EventRepository { get => _eventRepo; }
    }

    public sealed class UserRepo : RepositoryBase
    {
        public async IAsyncEnumerable<User> GetAllUsersAsync()
        {
            cmd = "SELECT * FROM users";
            using MySqlConnection connection = GetConnection();
            using MySqlCommand command = new(cmd, connection);
            await connection.OpenAsync();
            using MySqlDataReader reader = command.ExecuteReader();
            while (await reader.ReadAsync())
            {
                yield return new User
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Password = reader.GetString(2),
                    Points = reader.GetInt32(3),
                    Role = reader.GetInt32(4)
                };
            }
        }
        public async Task RegisterUserAsync(User user)
        {
            cmd = "INSERT into users(name,password,points,role) VALUES(@username,@password,@points,@role)";
            using MySqlConnection connection = GetConnection();
            using MySqlCommand command = new(cmd, connection);
            await connection.OpenAsync();
            command.Parameters.AddWithValue("@username", user.Name);
            command.Parameters.AddWithValue("@password", user.Password);
            command.Parameters.AddWithValue("@points", user.Points);
            command.Parameters.AddWithValue("@role", user.Role);
            await command.ExecuteNonQueryAsync();
        }
        public async Task ModifyUserAsync(User user)
        {
            cmd = "UPDATE TABLE users SET name=@name, password=@password, points=@points role=@role"
                + $"WHERE id={user.Id}";
            using MySqlConnection connection = GetConnection();
            using MySqlCommand command = new(cmd, connection);
            await connection.OpenAsync();
            command.Parameters.AddWithValue("@name", user.Name);
            command.Parameters.AddWithValue("@password", user.Password);
            command.Parameters.AddWithValue("@points", user.Points);
            command.Parameters.AddWithValue("@role", user.Role);
        }


    }
    public sealed class GameRepo : RepositoryBase
    {
        public async IAsyncEnumerable<Game> GetGamesAsync()
        {

            cmd = "SELECT g.id as game_id  g.name AS game_name, u.name AS organizer_name, g.status AS game_status" +
                "FROM  games g  JOIN users u ON g.userId = u.id JOIN participants p ON g.id = p.gameId ORDER BY g.name;";
            using MySqlConnection conn = GetConnection();
            using MySqlCommand command = new(cmd, conn);
            await conn.OpenAsync();
            using MySqlDataReader reader = command.ExecuteReader();
            while (await reader.ReadAsync())
            {
                int gameId = reader.GetInt32(0);
                Game game = new Game
                {
                    Id = gameId,
                    Name = reader.GetString(1),
                    OrganizerName = reader.GetString(2),
                    IsGameOver = reader.GetBoolean(5),
                };
                game.Participants = await GetParticipantsAsync(gameId);
                yield return game;
            }
        }
        public async Task<List<Participant>> GetParticipantsAsync(int ID)
        {
            cmd = $"SELECT id,name FROM participants WHERE INNER JOIN gameparticipants ON gameparticipants.participantid=participants.id" +
                $"WHERE gameparticipants.gameId={ID}";
            using MySqlConnection conn = GetConnection();
            using MySqlCommand command = new(cmd, conn);
            await conn.OpenAsync();
            using MySqlDataReader reader = command.ExecuteReader();
            List<Participant> Participants = [];
            while (await reader.ReadAsync())
            {
                Participants.Add(new() { Id = reader.GetInt32(0), Name = reader.GetString(0) });
            }
            return Participants;
        }
        public async Task CreateNewGameAsync(User user, Game game, List<Event> events)
        {
            cmd = "INSERT INTO games(name, userId, status) VALUES(@name,@userId,@status) ";
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
            cmd = $"UPDATE TABLE games SET status=@status WHERE id={game.Id}";
            using MySqlConnection conn = GetConnection();
            using MySqlCommand command = new(cmd, conn);
            await conn.OpenAsync();
            command.Parameters.AddWithValue("@status", game.IsGameOver);
            await command.ExecuteNonQueryAsync();
        }

    }
    public sealed class BetRepo : RepositoryBase
    {
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

    }
    public sealed class EventRepo : RepositoryBase
    {
        public async IAsyncEnumerable<Event> GetEventsAsync(Game game)
        {
            string cmd = $"SELECT id,eventName FROM events WHERE gameId={game.Id}";
            using MySqlConnection conn = GetConnection();
            using MySqlCommand command = new(cmd, conn);
            await conn.OpenAsync();
            using MySqlDataReader reader = command.ExecuteReader();
            while (await reader.ReadAsync())
            {
                Event _event = new Event
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                };
                yield return _event;

            }

        }
    }
    public sealed class ResultRepo : RepositoryBase 
    {
        public async IAsyncEnumerable<Result> GetResultsAsync(Game game)
        {
            cmd = $"";
            using MySqlConnection conn = GetConnection();
            using MySqlCommand command = new(cmd, conn);
            await conn.OpenAsync();
            using MySqlDataReader reader = command.ExecuteReader();
            while(await reader.ReadAsync())
            {
                Result result = new Result
                {

                };
                yield return result;
            }
        }

    }
}
