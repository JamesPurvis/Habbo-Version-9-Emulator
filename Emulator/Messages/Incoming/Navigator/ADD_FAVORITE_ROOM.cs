using Emulator.Game.Database;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Incoming.Navigator
{
    public class ADD_FAVORITE_ROOM : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            int m_room_type = r.popInteger();
            int m_room_id = r.popInteger();

            if (m_room_type == 1) m_room_id -= 1000;

            DatabaseManager.addFavoriteRoom(m_room_type, m_room_id, s.returnUser.user_id);
        }
    }
}
