using DotNetty.Transport.Channels;
using Emulator.Game.Models;
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
    public class APPROVENAME : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            string m_name = r.popString();

            s.SendToSession(new ApproveNameReply(m_name, s));
        }
    }
}
