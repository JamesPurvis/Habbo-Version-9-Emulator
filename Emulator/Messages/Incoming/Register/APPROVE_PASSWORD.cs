using DotNetty.Transport.Channels;
using Emulator.Messages.Outgoing;
using Emulator.Messages.Outgoing.Register;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Incoming.Register
{
    public class APPROVE_PASSWORD : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
           s.SendToSession(new PasswordApprovedReply());
        }
    }
}
