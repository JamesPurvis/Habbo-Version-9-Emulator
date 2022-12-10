using Emulator.Game.Database;
using Emulator.Game.Models;
using Emulator.Messages.Outgoing.Navigator;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Incoming.Navigator
{
    public class GETFVRF : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            IList<NavigatorFavorites> m_favorites = DatabaseManager.returnAllFavoriteRooms(s.returnUser.user_id);
            s.SendToSession(new FavoriteRoomResults(s,m_favorites));
        }
    }
}
