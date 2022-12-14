using Emulator.Messages.Outgoing.Room;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Incoming.Rooms
{
    public class G_HMAP : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
          s.SendToSession(new HeightmapReply(s));
        }
    }
}
