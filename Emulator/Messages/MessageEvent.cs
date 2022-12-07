using DotNetty.Transport.Channels;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages
{
    public interface MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s);
    }
    
}
