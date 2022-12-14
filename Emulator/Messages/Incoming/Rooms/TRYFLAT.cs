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
            //do doorbell stuff

            Room m_room_instance = null;

            try
            {
                 m_room_instance = Startup.return_environment().return_room_manager().returnRoomInstance(m_room_id);
            }

            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }


            s.return_room_instance = m_room_instance;

            s.SendToSession(new FlatLetInReply());
        }
    }
}
