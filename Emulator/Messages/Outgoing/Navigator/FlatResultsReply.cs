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
        private GameSession m_session = null;
        private String m_username = null;

        public FlatResultsReply(GameSession s, String username)
        {
            m_session = s;
            m_username = username;
        }
        public void compose(HabboResponse response)
        {
            response.return_special = true;
            IList<NavigatorPrivates> mRooms = DatabaseManager.returnRoomByOwner(m_username);

            foreach(NavigatorPrivates room in mRooms)
            {
                response.writeInt(room.room_id);
                response.write((char)9);
                response.writeString(room.room_name);
                response.write((char)9);
                response.writeString(room.room_owner);
                response.write((char)9);
                response.writeString(room.room_status);
                response.write((char)9);
                response.writeString("x");
                response.write((char)9);
                response.writeString(room.room_visitors);
                response.write((char)9);
                response.writeString(room.room_max_visitors);
                response.write((char)9);
                response.writeString("null");
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
