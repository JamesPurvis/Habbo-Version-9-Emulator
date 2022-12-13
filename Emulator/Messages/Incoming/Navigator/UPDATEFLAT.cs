using Emulator.Game.Database;
using Emulator.Game.Models;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Emulator.Messages.Incoming.Navigator
{
    public class UPDATEFLAT : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            string m_data = r.return_body();

            string[] m_room_details = m_data.Split('/');

            string m_room_name = m_room_details[1];
            string m_room_status = m_room_details[2];
            int m_room_show_owner = int.Parse(m_room_details[3]);

            NavigatorRooms m_room = DatabaseManager.return_user_room(int.Parse(m_room_details[0]));

            m_room.room_name = m_room_name;
            m_room.room_status = m_room_status;
            m_room.show_owner = m_room_show_owner == 1;

            DatabaseManager.UpdateUserRoom(m_room);
        }
    }
}
