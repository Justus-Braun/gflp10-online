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
        public static Dictionary<string, User> UserList = new Dictionary<string, User>();

        public Program()
        {
            EventHandlers["onResourceStart"] += new Action<string>(OnResourceStart);
            EventHandlers["framework:server:requestToken"] += new Action<Player>(OnGetToken);
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

        private void OnGetToken([FromSource] Player player)
        {
            User user = UserList[player.Handle];

            if (user.Token != null)
            {
                user.Player.Drop("Cheating");
            }


            string tempToken = TokenHandler.GeneradeToken();
            user.Token = tempToken + player.Handle;

            player.TriggerEvent("framework:client:sendToken", tempToken);
        }

    }
}
