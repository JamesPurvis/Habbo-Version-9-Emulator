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
    public class MESSENGER_ASSIGN_PER_MSG : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            String m_mission = r.popString();

            s.SendToSession(new MyPersistentMessageReply(s.returnUser.user_name, m_mission));
        }
    }
}
