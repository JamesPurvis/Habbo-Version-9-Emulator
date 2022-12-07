using Emulator.Game.Database;
using Emulator.Game.Messenger;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Outgoing.Messenger
{
    public class RemoveBuddyReply : MessageComposer
    {
        private int m_user_id;


        public RemoveBuddyReply(int userid)
        {
            this.m_user_id = userid;
        }
        public void compose(HabboResponse response)
        {

            response.writeInt(1);
            response.writeInt(m_user_id);

        }

        public short return_header_id()
        {
            return 138;
        }
    }
}
