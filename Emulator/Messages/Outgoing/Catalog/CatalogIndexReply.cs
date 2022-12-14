using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Outgoing.Catalog
{
    public class CatalogIndexReply : MessageComposer
    {
        public void compose(HabboResponse response)
        {

        }

        public short return_header_id()
        {
            return 126;
        }
    }
}
