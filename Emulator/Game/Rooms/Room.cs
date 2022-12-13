using Emulator.Game.Models;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Game.Rooms
{
    public class Room
    { 
        public int return_room_id { get; set; }
        public List<RoomUser> return_room_users { get; set; }
       
        public NavigatorRooms return_room_info { get; set; }

        public Room(int id, NavigatorRooms instance)
        {
            this.return_room_id = id;
            this.return_room_users = new List<RoomUser>();
            this.return_room_info = instance;
        }
    }
}
