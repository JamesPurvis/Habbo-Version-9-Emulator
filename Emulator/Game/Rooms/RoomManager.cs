using Emulator.Game.Models;
using NHibernate.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Game.Rooms
{
    public class RoomManager
    {
        private LinkedHashMap<int, Room> m_room_map;
        public RoomManager()
        {

        }

        public void loadRoomIntoMemory(Room instance)
        {
            if (m_room_map == null)
            {
                m_room_map = new LinkedHashMap<int, Room>();
            }

            m_room_map.Add(instance.return_room_id, instance);
        }

        public Room returnRoomInstance(int room_id)
        {
            return m_room_map[room_id];
        }

        public RoomUser returnNewRoomUser(UserModel u, Room instance)
        {
            RoomUser new_room_user = null;

            if (u != null)
            {
                new_room_user = new RoomUser(u);
            }

            return new_room_user;
        }

        public void mapToRoom(RoomUser r, Room instance)
        {
            instance.return_room_users.Add(r);
        }
    }
}
