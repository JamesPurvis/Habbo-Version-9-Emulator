using Emulator.Game.Database;
using Emulator.Game.Models;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using NHibernate.Cfg.Loquacious;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Outgoing.Navigator
{
    public class FlatResultsReply : MessageComposer
    {
        private IList<NavigatorRooms> m_room_list;

        public FlatResultsReply(IList<NavigatorRooms> m_room_list)
        {
            this.m_room_list = m_room_list;
        }
        public void compose(HabboResponse response)
        {

            foreach (NavigatorRooms room in m_room_list)
            {
                response.write(room.room_id);
                response.write((char)9);
                response.write(room.room_name);
                response.write((char)9);
                if (room.show_owner == true) response.writeString(room.room_owner);
                if (room.show_owner == false) response.writeString("-");
                response.write((char)9);
                response.write(room.room_status);
                response.write((char)9);
                response.write("x");
                response.write((char)9);
                response.write(room.room_visitors);
                response.write((char)9);
                response.write(room.room_max_visitors);
                response.write((char)9);
                response.write("null");
                response.write((char)9);
                response.write(room.room_description);
                response.write((char)9);
                response.write((char)13);

            }

        }

        public short return_header_id()
        {
            return 16;
        }
    }
}
