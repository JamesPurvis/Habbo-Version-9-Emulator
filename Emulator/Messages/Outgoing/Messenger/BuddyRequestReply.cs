using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Outgoing.Messenger
{
    public class BuddyRequestReply : MessageComposer
    {
        private int m_user_id;
        private string m_user_name;
        public BuddyRequestReply(int id, String name)
        {
            this.m_user_id = id;
            this.m_user_name = name;
        }
        public void compose(HabboResponse response)
        {
            response.writeInt(m_user_id);
            response.writeString(m_user_name);
        }

        public short return_header_id()
        {
            return 132;
        }
    }
}
