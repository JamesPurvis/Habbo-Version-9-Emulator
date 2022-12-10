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
    public class GETFLATINFO : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            s.SendToSession(new FlatInfoReply(int.Parse(r.return_body())));
        }
    }
}
