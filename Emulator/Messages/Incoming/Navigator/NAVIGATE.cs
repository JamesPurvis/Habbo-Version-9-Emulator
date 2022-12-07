using DotNetty.Transport.Channels;
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
    public class NAVIGATE : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            int m_show_Full = r.popInteger();
            int m_category_id = r.popInteger();

            s.SendToSession(new NavNodeInfoReply(m_show_Full, m_category_id, s));
        }
    }
}
