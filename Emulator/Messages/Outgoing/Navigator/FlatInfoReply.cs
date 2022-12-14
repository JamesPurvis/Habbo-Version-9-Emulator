using Emulator.Game.Database;
using Emulator.Game.Models;
using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Outgoing.Navigator
{
    public class FlatInfoReply : MessageComposer
    {
        private int room_id;
        public FlatInfoReply(int room_id)
        {
            this.room_id = room_id;
        }
        public void compose(HabboResponse response)
        {
            
            NavigatorRooms m_room = DatabaseManager.return_user_room(room_id);

              if (m_room.room_all_super == true) response.writeInt(1);
              if (m_room.room_all_super == false) response.writeInt(0);
              if (m_room.room_status == "open") response.writeInt(0);
              if (m_room.room_status == "closed") response.writeInt(1);
              if (m_room.room_status == "password") response.writeInt(2);
              response.writeInt(m_room.room_id);
              response.writeString(m_room.room_owner);
              response.writeString(m_room.room_model);
              response.writeString(m_room.room_name);
              response.writeString(m_room.room_description);
              response.writeInt(Convert.ToInt32(m_room.show_owner));
              response.writeInt(0);
              response.writeInt(Convert.ToInt32(m_room.room_category_id == 0));
              response.writeInt(m_room.room_max_visitors);
              response.writeInt(m_room.room_max_visitors);



        }

        public short return_header_id()
        {
            return 54;
        }
    }
}
