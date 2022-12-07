using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages
{

    public interface MessageComposer
    {
        public short return_header_id();
        public void compose(HabboResponse response);
    }
}
