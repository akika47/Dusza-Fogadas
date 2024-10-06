
﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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
            

            string cmd = "INSERT into users VALUES(?,?,?,?)";
            using MySqlConnection connection = GetConnection();
            using MySqlCommand command = new(cmd, connection);
            await connection.OpenAsync();
            command.Parameters.AddWithValue("Username", user.Name);
            command.Parameters.AddWithValue("Password", user.Password);
            command.Parameters.AddWithValue("Points", 100);
            command.Parameters.AddWithValue("Role", user.Role);
            await command.ExecuteNonQueryAsync();

        }

    }
}
