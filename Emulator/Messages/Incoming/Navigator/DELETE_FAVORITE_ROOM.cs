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
    public class DELETE_FAVORITE_ROOM : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            int m_room_type = r.popInteger();
            int m_room_id = r.popInteger();

            NavigatorFavorites m_instance = DatabaseManager.returnFavoriteRoom(m_room_id, m_room_type, s.returnUser.user_id);

            DatabaseManager.deleteFavoriteRoom(m_instance);
        }
    }
}
