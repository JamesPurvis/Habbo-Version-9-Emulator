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
    public class GETUSERFLATCATS : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            s.SendToSession(new UserFlatCatsReply());
        }
    }
}
