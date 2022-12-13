using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Outgoing.Room.Users
{
    public class ChatReply : MessageComposer
    {
        private int m_id;
        private string m_text;

        public ChatReply(int id, string text)
        {
            m_id = id;
            m_text = text;
        }
        public void compose(HabboResponse response)
        {
            response.writeInt(m_id);
            response.writeString(m_text);
        }

        public short return_header_id()
        {
            return 24;
        }
    }
}
