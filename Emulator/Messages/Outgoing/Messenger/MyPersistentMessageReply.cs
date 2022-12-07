using Emulator.Game.Database;
using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Outgoing.Messenger
{
    public class MyPersistentMessageReply : MessageComposer
    {
        private string m_msg;
        private string m_user_name;

        public MyPersistentMessageReply(String name, String mission)
        {
            this.m_msg = mission;
            this.m_user_name = name;
        }
        public void compose(HabboResponse response)
        {
            response.writeString(m_msg);
            DatabaseManager.updatePersistentMessage(m_user_name, m_msg);
        }

        public short return_header_id()
        {
            return 147;
        }
    }
}
