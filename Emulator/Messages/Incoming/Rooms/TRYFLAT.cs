using Emulator.Game.Database;
using Emulator.Game.Models;
using Emulator.Game.Rooms;
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
    public class TRYFLAT : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            int m_room_id = int.Parse(r.return_body());
            NavigatorRooms m_room_model = DatabaseManager.return_user_room(m_room_id);
            //do doorbell stuff

            Room m_room_instance = new Room(m_room_id, m_room_model);

            s.return_room_instance = m_room_instance;

            s.SendToSession(new FlatLetInReply());
        }
    }
}
