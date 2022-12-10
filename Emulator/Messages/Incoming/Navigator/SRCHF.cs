using Emulator.Game.Database;
using Emulator.Game.Models;
using Emulator.Messages.Outgoing.Navigator;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Incoming.Navigator
{
    public class SRCHF : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            string m_search_term = r.return_body();

            IList<NavigatorPrivates> m_search_result = DatabaseManager.searchForRooms(m_search_term);

            if (m_search_result.Count > 0)
            {
                s.SendToSession(new FlatResultsSrcReply(m_search_result));
            }
            else
            {
                s.SendToSession(new NoFlatsReply());
            }
        }
    }
}
