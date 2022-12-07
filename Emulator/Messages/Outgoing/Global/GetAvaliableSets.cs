using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Outgoing.Global
{
    public class GetAvaliableSets : MessageComposer
    {
        public void compose(HabboResponse response)
        {
            response.writeString("[100, 200, 300, 400, 500, 600]");
        }

        public short return_header_id()
        {
            return 8;
        }
    }
}
