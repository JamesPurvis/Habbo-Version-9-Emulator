using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Outgoing.Navigator
{
    public class FlatCreatedReply : MessageComposer
    {
        private int m_id;
        private string m_room_name;
        public FlatCreatedReply(int id, string name)
        {
            this.m_id = id;
            this.m_room_name = name;
        }
        public void compose(HabboResponse response)
        {
            response.writeInt(m_id);
            response.writeString(m_room_name);
        }

        public short return_header_id()
        {
            return 59;
        }
    }
}
