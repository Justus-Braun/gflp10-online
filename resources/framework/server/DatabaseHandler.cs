using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using MySqlConnector;
using server.classes;

namespace server
{
    public class DatabaseHandler
    {
        private static string _conString = API.GetConvar("mysql_connection_string", "");

        public static async Task<User> GetUser(Player player)
        {
            User u = new User(player);

            var connection = new MySqlConnection(_conString);
            
            connection.Open();

            var command = connection.CreateCommand();
            
            command.CommandText = "SELECT * FROM tblUser WHERE identifier = @identifier;";
            command.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@identifier",
                DbType = DbType.String,
                Value = u.Player.Identifiers["license"]
            });

            var reader = await command.ExecuteReaderAsync();
                    
            if (!reader.HasRows)
            {
                throw new NotSupportedException();
            }

            if (reader.Read())
            {
                u.Money = new Account(reader.GetInt32("money"));
                u.Bank = new Account(reader.GetInt32("bank"));
            } 
            
            connection.Dispose();

            return u;
        }

        public static async Task<Task> UpdatePlayer(User user)
        {
            var connection = new MySqlConnection(_conString);
            
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "UPDATE tblUser SET money = @money, bank = @bank WHERE identifier = @identifier;";
            command.Parameters.AddRange(new[]
                {
                    new MySqlParameter
                    {
                        ParameterName = "@identifier",
                        DbType = DbType.String,
                        Value = user.Player.Identifiers["license"]
                    },
                    new MySqlParameter
                    {
                        ParameterName = "@money",
                        DbType = DbType.Int32,
                        Value = user.Money.Balance
                    },
                    new MySqlParameter
                    {
                        ParameterName = "@bank",
                        DbType = DbType.Int32,
                        Value = user.Bank.Balance
                    }
                }
            );

            await command.ExecuteNonQueryAsync();

            connection.Dispose();

            return Task.CompletedTask;
        }

        public static async Task<Task> InsertPlayer(User user)
        {
            var connection = new MySqlConnection(_conString);
            connection.Open();

            var command = connection.CreateCommand();
                
            command.CommandText = "INSERT INTO tblUser (identifier, money, bank) VALUES (@identifier, @money, @bank);";
            command.Parameters.AddRange(new[]
                {
                    new MySqlParameter
                    {
                        ParameterName = "@identifier",
                        DbType = DbType.String,
                        Value = user.Player.Identifiers["license"]
                    },
                    new MySqlParameter
                    {
                        ParameterName = "@money",
                        DbType = DbType.Int32,
                        Value = user.Money.Balance
                    },
                    new MySqlParameter
                    {
                        ParameterName = "@bank",
                        DbType = DbType.Int32,
                        Value = user.Bank.Balance
                    }
                }
            );

            await command.ExecuteNonQueryAsync();

            connection.Dispose();

            return Task.CompletedTask;
        }
    }
}
