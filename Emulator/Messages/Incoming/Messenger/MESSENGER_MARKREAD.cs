using Emulator.Game.Database;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Incoming.Messenger
{
    public class MESSENGER_MARKREAD : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            int m_id = r.popInteger();

            DatabaseManager.markMessageAsRead(m_id);
        }
    }
}
