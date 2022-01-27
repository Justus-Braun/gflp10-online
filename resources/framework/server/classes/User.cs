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
        public int Id { get; set; }
        public Account Money { get; set; }
        public Account Bank { get; set; }
        public List<Weapon> Weapons { get; set; }
    }
}
