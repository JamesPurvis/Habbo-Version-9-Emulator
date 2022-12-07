using DotNetty.Transport.Channels;
using Emulator.Messages.Outgoing.Purse;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Incoming.Purse
{
    public class GET_CREDITS : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            s.SendToSession(new GetWalletBalanceReply(s));
        }
    }
}
