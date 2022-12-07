using Emulator.Messages.Outgoing.Messenger;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Incoming.Messenger
{
    public class MESSENGER_FIND_USER : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            String m_user_name = r.popString();

            s.SendToSession(new MemberInfoReply(m_user_name));
        }
    }
}
