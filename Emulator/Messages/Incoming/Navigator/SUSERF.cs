using Emulator.Game.Database;
using Emulator.Game.Models;
using Emulator.Messages.Outgoing.Navigator;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Incoming.Navigator
{
    public class SUSERF : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            IList<NavigatorPrivates> m_room_list = DatabaseManager.returnRoomByOwner(s.returnUser.user_name);

            if (m_room_list.Count > 0)
            {
                s.SendToSession(new FlatResultsReply(m_room_list));
            }
            else
            {
                s.SendToSession(new NoFlatsForUser());
            }
        }
    }
}
