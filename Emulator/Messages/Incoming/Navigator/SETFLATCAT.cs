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
    public class SETFLATCAT : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            int m_room_id = r.popInteger();
            int m_category_id = r.popInteger();

            NavigatorPrivates m_room = DatabaseManager.return_user_room(m_room_id);
            m_room.room_category_id = m_category_id;
            DatabaseManager.UpdateUserRoom(m_room);
        }
    }
}
