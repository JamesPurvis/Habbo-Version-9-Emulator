using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Outgoing.Room
{
    public class FlatPropertyReply : MessageComposer
    {
        private string m_property;
        private int m_id;

        public FlatPropertyReply(string prop, int id)
        {
            this.m_property = prop;
            this.m_id = id;
        }
        public void compose(HabboResponse response)
        {
            response.write(m_property);
            response.write("/");
            response.write(m_id);
        }

        public short return_header_id()
        {
            return 46;
        }
    }
}
