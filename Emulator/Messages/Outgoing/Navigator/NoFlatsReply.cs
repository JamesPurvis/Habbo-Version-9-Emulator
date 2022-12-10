using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Outgoing.Navigator
{
    public class NoFlatsReply : MessageComposer
    {
        public void compose(HabboResponse response)
        {
            
        }

        public short return_header_id()
        {
            return 58;
        }
    }
}
