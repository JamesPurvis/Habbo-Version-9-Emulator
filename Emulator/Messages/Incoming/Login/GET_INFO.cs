using Emulator.Messages.Outgoing.Login;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Incoming.Login
{
    public class GET_INFO : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            s.SendToSession(new UserObjectReply(s.returnUser));

          

        }
    }
}
