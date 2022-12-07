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
            String m_userName = s.returnUser.user_name;
            s.SendToSession(new FlatResultsReply(s, m_userName));
        }
    }
}
