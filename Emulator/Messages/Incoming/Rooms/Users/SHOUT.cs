using Emulator.Messages.Outgoing.Room;
using Emulator.Messages.Outgoing.Room.Users;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Incoming.Rooms.Users
{
    public class SHOUT : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            string m_text = r.popString();
            int m_id = s.returnUser.user_id;

           s.return_room_instance.SendToRoom(new ShoutReply(m_id, m_text));
        }
    }
}
