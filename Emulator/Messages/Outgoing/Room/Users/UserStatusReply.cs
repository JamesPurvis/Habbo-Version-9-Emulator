using Emulator.Network.Session;
using Emulator.Network.Streams;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Outgoing.Room.Users
{
    public class UserStatusReply : MessageComposer
    {

        private string m_status;
        public UserStatusReply(String status)
        {
            this.m_status = status;
        }

        public void compose(HabboResponse response)
        {
            response.writeString(m_status);
        }

        public short return_header_id()
        {
            return 34;
        }
    }
}
