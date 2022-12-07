using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Outgoing.Global
{
    public class HotelAlertReply : MessageComposer
    {
        private string m_message;

        public HotelAlertReply(String msg)
        {
            this.m_message = msg;
        }
        public void compose(HabboResponse response)
        {
            response.writeString(m_message);
        }

        public short return_header_id()
        {
            return 139;
        }
    }
}
