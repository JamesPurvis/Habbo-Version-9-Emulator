using Emulator.Network.Streams;
using Emulator.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Outgoing.Navigator
{
    public class FlatCatReply : MessageComposer
    {
        private int m_room_id;
        private int m_category_id;

        public FlatCatReply(int m_room_id, int m_category_id)
        {
            this.m_room_id = m_room_id;
            this.m_category_id = m_category_id;
        }
        public void compose(HabboResponse response)
        {
            response.writeInt(m_room_id);
            response.writeInt(m_category_id);
        }

        public short return_header_id()
        {
            return 222;
        }
    }
}
