using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace server.classes
{
    public class User
    {
        public User(Player player)
        {
            Player = player;
        }
        public Player Player { get; set; }
        public Account Money { get; set; }
        public Account Bank { get; set; }
        public List<Weapon> Weapons { get; set; }
        public string Token { get; set; }
    }
}
