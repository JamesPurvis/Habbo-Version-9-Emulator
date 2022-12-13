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
    public class GOTOFLAT : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            s.SendToSession(new RoomUrlReply());
            s.SendToSession(new RoomReadyReply(s.return_room_instance));
            s.SendToSession(new FlatPropertyReply("wallpaper", 0));
            s.SendToSession(new FlatPropertyReply("floor", 0));
        }
    }
}
