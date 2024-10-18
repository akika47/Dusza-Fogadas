using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;
using System.Text;
using WPF_Dusza.Models;
namespace WPF_Dusza.Repo
{
    public sealed class BettingRepository
    {
        readonly UserRepo _userRepo = new();
        readonly GameRepo _gameRepo = new();
        readonly BetRepo _betRepo = new();
        readonly EventRepo _eventRepo = new();
        readonly ResultRepo _resultRepo = new();


        public UserRepo UserRepository { get => _userRepo; }
        public GameRepo GameRepository { get => _gameRepo; }
        public BetRepo BetRepository { get => _betRepo; }
        public EventRepo EventRepository { get => _eventRepo; }

        public ResultRepo ResultRepository { get => _resultRepo; }

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
        public async Task DeleteUserAsync(int id)
        {
            cmd = $"DELETE FROM users WHERE id={id}";
            using MySqlConnection conn = GetConnection();
            using MySqlCommand command = new(cmd, conn);
            await conn.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
    public sealed class GameRepo : RepositoryBase
    {
        public async IAsyncEnumerable<Game> GetGamesAsync()
        {

            cmd = "SELECT g.id, g.name, u.name AS organizer_name, g.status AS current_status FROM games g JOIN users u ON g.userId = u.id LEFT JOIN gameparticipants gp ON g.id = gp.gameId GROUP BY g.id, g.name, u.name;";


            using MySqlConnection conn = GetConnection();
            using MySqlCommand command = new(cmd, conn);
            await conn.OpenAsync();
            using MySqlDataReader reader = command.ExecuteReader();
            while (await reader.ReadAsync())
            {
                int gameId = reader.GetInt32(0);
                Game game = new()
                {
                    Id = gameId,
                    Name = reader.GetString(1),
                    OrganizerName = reader.GetString(2),
                    IsGameOver = reader.GetBoolean(3),
                    Participants = await GetParticipantsAsync(gameId)
                };
                yield return game;


            }
        }
        public async Task<List<GameRow>> GetGamesAsync(User user)
        {
            List<GameRow> result = [];
            var allGames = await GetGamesAsync().Where(x => x.OrganizerName == user.Name).ToListAsync();
            foreach (Game game in allGames)
            {
                GameRow row = new()
                {

                    GameName = game.Name,
                    OrganizerName = user.Name,
                    Participants = string.Join("\n", await GetParticipantsAsync(game.Id)),
                    Events = string.Join("\n", await GetEventsAsync(game.Id).ToListAsync()),
                    IsDisplay = true
                };
                result.Add(row);
            }
            return result;
        }
        async IAsyncEnumerable<Event> GetEventsAsync(int ID)
        {
            cmd = $"SELECT id,eventName FROM events WHERE gameId={ID}";
            using MySqlConnection conn = GetConnection();
            await conn.OpenAsync();
            using MySqlCommand command = new(cmd, conn);
            using MySqlDataReader reader = command.ExecuteReader();
            while (await reader.ReadAsync())
            {
                Event @event = new()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                };
                yield return @event;
            }
        }

        public async Task<List<Participant>> GetParticipantsAsync(int ID)
        {
            cmd = $"SELECT id,name FROM participants INNER JOIN gameparticipants ON gameparticipants.participantid=participants.id WHERE gameparticipants.gameId={ID};";
            using MySqlConnection conn = GetConnection();
            using MySqlCommand command = new(cmd, conn);
            await conn.OpenAsync();
            using MySqlDataReader reader = command.ExecuteReader();
            List<Participant> Participants = [];
            while (await reader.ReadAsync())
            {
                Participants.Add(new() { Id = reader.GetInt32(0), Name = reader.GetString(1) });

            }
            return Participants;
        }



        public async Task CreateNewGameAsync(User user, Game game, List<Event> events, List<Participant> participants)
        {
            cmd = "INSERT INTO games(name, userId, status) VALUES(@name,@userId,@status) ";
            using MySqlConnection conn = GetConnection();
            using MySqlCommand command = new(cmd, conn);
            await conn.OpenAsync();
            command.Parameters.AddWithValue("@name", game.Name);
            command.Parameters.AddWithValue("@userId", user.Id);
            command.Parameters.AddWithValue("@status", game.IsGameOver);
            await command.ExecuteNonQueryAsync();
            await AddNewEventsAsync(conn, events, game.Id);

        }

        async Task AddNewEventsAsync(MySqlConnection conn, List<Event> events, int gameId)
        {
            // Batch insert
            cmd = $"INSERT INTO events(eventName, gameId) VALUES ";
            List<string> rows = [];
            foreach (Event e in events)
            {
                rows.Add($"('{MySqlHelper.EscapeString(e.Name)}',{gameId})");
            }
            cmd += string.Join(",", rows) + ";";
            using MySqlCommand command = new(cmd, conn);
            await command.ExecuteNonQueryAsync();
        }
        public async Task AddNewParticipantsAsync(MySqlConnection conn, List<Participant> participants, int gameId)
        {
            using MySqlCommand command = new();
            command.Connection = conn;
            foreach (Participant participant in participants)
            {
                command.CommandText = $"INSERT INTO participants(name) VALUES('{MySqlHelper.EscapeString(participant.Name)}')";
                await command.ExecuteNonQueryAsync();
                participant.Id = (int)command.LastInsertedId;
            }
            command.CommandText = $"INSERT INTO gameparticipants(gameId, participantId) VALUES ";
            foreach (Participant participant in participants)
            {
                command.CommandText += $"({gameId}, {participant.Id}),"; 
            }
            command.CommandText.Remove(command.CommandText.Length - 1, 1);
            command.CommandText += ';';
            await command.ExecuteNonQueryAsync();
        }
        public async Task CloseGameAsync(Game game)
        {
            cmd = $"UPDATE TABLE games SET status={game.IsGameOver} WHERE id={game.Id}";
            using MySqlConnection conn = GetConnection();
            await conn.OpenAsync();
            using MySqlCommand command = new(cmd, conn);
            await command.ExecuteNonQueryAsync();
        }

    }
    public sealed class BetRepo : RepositoryBase
    {
        public async Task PlaceBetAsync(Bet bet)
        {
            string cmd = "INSERT INTO  bets(eventId,userId,participantId,prediction,betAmount) " +
                " VALUES(@eventId,@userId, @participantId, @prediction, @betAmount)";
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
                Event _event = new()
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

            cmd = $"SELECT e.eventName AS event_name, u.name AS user_name, b.prediction AS user_prediction " +
                $"FROM events AS e JOIN gameparticipants AS gp ON e.id = gp.gameld " +
                $"JOIN bets AS b ON gp.participantid = b.participantid JOIN users AS u ON " +
                $"b.userld = u.id where gp.gameId={game.Id}";

            using MySqlConnection conn = GetConnection();
            using MySqlCommand command = new(cmd, conn);
            await conn.OpenAsync();
            using MySqlDataReader reader = command.ExecuteReader();
            while (await reader.ReadAsync())
            {

                Result result = new()
                {
                    EventName = reader.GetString(0),
                    Prediction = reader.GetString(1),
                    EventResult = reader.GetString(2)
                };
                yield return result;
            }
        }

    }
}
