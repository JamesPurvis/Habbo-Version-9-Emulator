using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Outgoing.Global
{
    public class GetSessionParameters : MessageComposer
    {
        public void compose(HabboResponse response)
        {
            response.writeInt(0);
            response.writeInt(0);
            response.writeInt(0);
            response.writeInt(0);
            response.writeInt(0);
            response.writeString("dd-mm-yyyy");
        }

        public short return_header_id()
        {
            return 257;
        }
    }
}
