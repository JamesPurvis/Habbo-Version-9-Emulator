using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using Emulator.Messages.Outgoing;
using Emulator.Messages.Outgoing.Global;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Incoming.Global
{
    public class GENERATEKEY : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            s.SendToSession(new GetSessionParameters());
            s.SendToSession(new GetAvaliableSets());
        }
    }
}
