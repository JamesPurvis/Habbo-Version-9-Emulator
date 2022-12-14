using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Outgoing.Catalog
{
    public class CatalogPageReply : MessageComposer
    {
        private int page_id;
        public CatalogPageReply(int page_id)
        {
            this.page_id = page_id;
        }
        public void compose(HabboResponse response)
        {
            
        }

        public short return_header_id()
        {
            return 127;
        }
    }
}
