using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using MySqlConnector;
using server.classes;


namespace server
{
    public class JoinHandler : BaseScript
    {
        public JoinHandler()
        {
            EventHandlers["playerConnecting"] += new Action<Player, string, dynamic, dynamic>(OnPlayerConnecting);
            EventHandlers["playerJoining"] += new Action<Player, string>(OnPlayerJoin);
        }

        private async void OnPlayerConnecting([FromSource] Player player, string playerName, dynamic setKickReason, dynamic deferrals)
        {
            deferrals.defer();

            // mandatory wait!
            await Delay(0);

            var licenseIdentifier = player.Identifiers["license"];

            Debug.WriteLine($"A player with the name {playerName} (Identifier: [{licenseIdentifier}]) is connecting to the server.");

            deferrals.update($"Hello {playerName}, your license [{licenseIdentifier}] is being checked");
            deferrals.done();
        }

        private async void OnPlayerJoin([FromSource] Player player, string oldId)
        {
            Debug.WriteLine($"Player with ID: {player.Handle} passed the Bannlist");

            await Delay(0);
            
            try
            {
                User u = await DatabaseHandler.GetUser(player);
            }
            catch (NotSupportedException)
            {
                User u = new User
                {
                    Id = int.Parse(player.Handle),
                    Bank = new Account(10000),
                    Money = new Account(100),
                    Weapons = new List<Weapon>()
                };

                Program.UserList.Add(u);

                player.TriggerEvent("framework:client:firstJoin");

                return;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }

            player.TriggerEvent("framework:client:init");
        }
    }
}
