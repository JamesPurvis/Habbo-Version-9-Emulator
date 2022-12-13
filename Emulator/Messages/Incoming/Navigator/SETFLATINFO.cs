using Emulator.Game.Database;
using Emulator.Game.Models;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Incoming.Navigator
{
    public class SETFLATINFO : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            string m_data = r.return_body();

            if (m_data.StartsWith("/"))
            {
                m_data = m_data.Substring(1);
            }


            int m_room_id = int.Parse(m_data.Split('/')[0]);

            string[] roomDetails = m_data.Split(Convert.ToChar(13).ToString());


           string m_description = roomDetails[1].Split('=')[1];
           int m_all_super = int.Parse(roomDetails[3].Split('=')[1]);
          string m_password = roomDetails[2].Split('=')[1];

            NavigatorRooms m_room = DatabaseManager.return_user_room(m_room_id);

            m_room.room_description = m_description;
            m_room.room_all_super = m_all_super == 1;
            m_room.room_password = m_password;
            m_room.room_owner = s.returnUser.user_name;

            DatabaseManager.UpdateUserRoom(m_room);

        }
    }
}
