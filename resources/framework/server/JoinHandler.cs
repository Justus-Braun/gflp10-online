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
            EventHandlers["playerDropped"] += new Action<Player, string>(OnPlayerDropped);
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

            User user;
            try
            {
                user = await DatabaseHandler.GetUser(player);
            }
            catch (NotSupportedException)
            {
                user = new User(player)
                {
                    Bank = new Account(10000),
                    Money = new Account(100),
                    Weapons = new List<Weapon>()
                };

                try
                {
                    await DatabaseHandler.InsertPlayer(user);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    Debug.WriteLine(e.StackTrace);
                    player.Drop("Something went wrong please contact the Support");
                    return;
                }

                user = await DatabaseHandler.GetUser(player);
                
                Program.UserList.Add(player.Handle, user);
                player.TriggerEvent("framework:client:firstJoin");

                return;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
                return;
            }

            Program.UserList.Add(player.Handle, user);
            player.TriggerEvent("framework:client:init");
        }

        private async void OnPlayerDropped([FromSource] Player player, string reason)
        {
            try
            {
                // Save Player
                await DatabaseHandler.UpdatePlayer(Program.UserList[player.Handle]);

                // Remove Player from Server cache
                Debug.WriteLine($"Player {player.Name} dropped (Reason: {reason}).");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.Source);
            }
        }

    }
}
