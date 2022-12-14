using Emulator.Messages.Outgoing.Catalog;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Incoming.Catalog
{
    public class GCAP : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            int m_page_id = r.popInteger();

            s.SendToSession(new CatalogPageReply(m_page_id));
        }
    }
}
