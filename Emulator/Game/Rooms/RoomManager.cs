using Emulator.Game.Database;
using Emulator.Game.Models;
using Emulator.Network.Session;
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
            m_room_map = new LinkedHashMap<int, Room>();
        }

        public void loadRoomIntoMemory(Room instance)
        {
            m_room_map.Add(instance.return_room_id, instance);
        }

        public Room returnRoomInstance(int room_id)
        {
            NavigatorRooms instance = DatabaseManager.return_user_room(room_id);
            Room m_room;

            if (!m_room_map.Contains(room_id))
            {
                m_room = new Room(room_id, instance);
                loadRoomIntoMemory(m_room);
            }
            else
            {
                m_room = m_room_map[room_id];
            }

            return m_room;
        }

        public RoomUser returnNewRoomUser(GameSession s, UserModel u, Room instance)
        {
            RoomUser new_room_user = null;

            if (u != null)
            {
                new_room_user = new RoomUser(u);
            }

            s.return_room_user = new_room_user;
            s.return_room_user.m_game_session = s;

            instance.return_room_users.Add(new_room_user);

            return new_room_user;
        }

    }
}
