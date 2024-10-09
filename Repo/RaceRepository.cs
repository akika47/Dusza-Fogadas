
﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using WPF_Dusza.Models;
using WPF_Dusza.Utils;

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
            string cmd = "SELECT g.name AS game_name, u.name AS organizer_name,  p.name AS participant_name, g.status AS game_status" +
                "FROM  games g  JOIN users u ON g.userId = u.id JOIN participants p ON g.id = p.gameId ORDER BY g.name;";
            using MySqlConnection conn = GetConnection();
            using MySqlCommand command = new(cmd, conn);
            conn.Open();
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) 
            {
                yield return new Game
                {
                    Name = reader.GetString(0),
                    OrganizerName = reader.GetString(1),
                    FirstParticipant = reader.GetString(2),
                    SecondParticipant = reader.GetString(3),
                    IsGameOver = reader.GetBoolean(4),
                };
            }
        }
        public async Task PlaceBetAsync(User user)
        {
            string cmd = "";
            using MySqlConnection conn = GetConnection();
            using MySqlCommand command = new(cmd, conn);
            await conn.OpenAsync();
        }
        public async Task CreateNewGameAsync()
        {
            string cmd = "";
            using MySqlConnection conn = GetConnection();
            using MySqlCommand command = new(cmd, conn);
            await conn.OpenAsync();

        }
    }
}
