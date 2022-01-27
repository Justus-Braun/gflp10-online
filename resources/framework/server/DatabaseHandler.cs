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
        public static async Task<User> GetUser(Player player)
        {
            User u = new User
            {
                Id = int.Parse(player.Handle)
            };

            using (var connection = new MySqlConnection(API.GetConvar("mysql_connection_string", "")))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM tblUser WHERE identifier = @identifier;";
                    command.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "@identifier",
                        DbType = DbType.String,
                        Value = player.Identifiers["license"]
                    });

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (!reader.HasRows)
                        {
                            throw new NotSupportedException();
                        }

                        if (reader.Read())
                        {
                            u.Money = new Account(reader.GetInt32("money"));
                            u.Bank = new Account(reader.GetInt32("bank"));
                        }
                    }
                }
            }

            return u;
        }
    }
}
