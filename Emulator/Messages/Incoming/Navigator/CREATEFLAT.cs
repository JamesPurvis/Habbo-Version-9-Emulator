using Emulator.Messages.Outgoing.Navigator;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using NHibernate.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Incoming.Navigator
{
    public class CREATEFLAT : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            String m_create_string = r.toString();

            LinkedHashMap<int, string> m_props = Utils.RoomPacketParser.returnRoomProperties(m_create_string);

            String m_room_category = m_props[1];
            String m_room_name = m_props[2];
            String m_room_model = m_props[3];
            String m_room_status = m_props[4];
            int m_room_show_owner = int.Parse(m_props[5]);

           

            s.SendToSession(new FlatCreatedReply(1, m_room_name));
        }
    }
}
