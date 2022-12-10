using Emulator.Game.Database;
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
    public class GETFLATCAT : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            int m_room_id = r.popInteger();
            int m_category_id = DatabaseManager.return_user_room(m_room_id).room_category_id;

            s.SendToSession(new FlatCatReply(m_room_id, m_category_id));
        }
    }
}
