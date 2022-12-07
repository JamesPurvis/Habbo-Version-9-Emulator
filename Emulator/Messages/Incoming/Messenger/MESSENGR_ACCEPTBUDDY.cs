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
    //TODO: When a messenger request is accepted the request isn't deleted for some reason.
    public class MESSENGER_ACCEPTBUDDY : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            int m_id = r.popInteger();

            s.SendToSession(new AcceptBuddyReply(m_id, s));
        }


    }
}
