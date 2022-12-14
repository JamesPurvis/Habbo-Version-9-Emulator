using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emulator.Game.Rooms;

namespace Emulator.Messages.Outgoing.Room
{
    public class RoomReadyReply : MessageComposer
    {
        private Emulator.Game.Rooms.Room m_instance;
        public RoomReadyReply(Emulator.Game.Rooms.Room instance)
        {
            this.m_instance = instance;
        }
        public void compose(HabboResponse response)
        {
            response.writeString(m_instance.return_room_info.room_model);
            response.writeString(" ");
            response.writeInt(m_instance.return_room_info.room_id);
        }

        public short return_header_id()
        {
            return 69;
        }
    }
}
