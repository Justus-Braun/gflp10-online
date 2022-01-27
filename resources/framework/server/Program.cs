using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using MySqlConnector;
using server.classes;

namespace server
{
    public class Program : BaseScript
    {
        public static List<User> UserList = new List<User>();

        public Program()
        {
            EventHandlers["onResourceStart"] += new Action<string>(OnResourceStart);
        }

        private void OnResourceStart(string resourceName)
        {
            if (API.GetCurrentResourceName() != resourceName) return;
            Debug.WriteLine($"{resourceName} has been started. Developed by GFLP10#7754");

            using (var connection = new MySqlConnection(API.GetConvar("mysql_connection_string", "")))
            {
                connection.Open();
                Debug.WriteLine(connection.Ping() ? "Database connection created" : "Database connection Failed");
            }
        }

    }
}
