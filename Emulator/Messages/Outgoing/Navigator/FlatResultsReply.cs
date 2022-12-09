using Emulator.Game.Database;
using Emulator.Game.Models;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Outgoing.Navigator
{
    public class FlatResultsReply : MessageComposer
    {
        private IList<NavigatorPrivates> m_room_list;

        public FlatResultsReply(IList<NavigatorPrivates> m_room_list)
        {
            this.m_room_list = m_room_list;
        }
        public void compose(HabboResponse response)
        {

            foreach (NavigatorPrivates room in m_room_list)
            {
                response.writeInt(room.room_id);
                response.write((char)9);
                response.write(room.room_name);
                response.write((char)9);
                response.write(room.room_owner);
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
