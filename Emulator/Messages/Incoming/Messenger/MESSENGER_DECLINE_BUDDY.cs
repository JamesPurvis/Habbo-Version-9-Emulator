using Emulator.Game.Database;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Incoming.Messenger
{
    public class MESSENGER_DECLINE_BUDDY : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            int m_mode = r.popInteger();

            if (m_mode > 0)
            {
                int m_removal_id = r.popInteger();
                DatabaseManager.removeFriendRequest(m_removal_id, s.returnUser.user_id);
            }
            else
            {
                DatabaseManager.removeAllFriendRequests(s.returnUser.user_id);
            }

       
        }
    }
}
