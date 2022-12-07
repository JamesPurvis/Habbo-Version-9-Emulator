using Emulator.Game.Database;
using Emulator.Messages.Outgoing.Messenger;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Incoming.Messenger
{
    public class MESSENGER_REMOVE_BUDDY : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            r.popInteger();

            int m_removal_id = r.popInteger();
            String m_user_name = DatabaseManager.returnEntityById(m_removal_id).user_name;

            if (Startup.return_environment().return_session_manager().checkIfConnected(m_user_name))
            {
                Startup.return_environment().return_session_manager().returnGameSession(m_user_name).SendToSession(new RemoveBuddyReply(s.returnUser.user_id));
            }

            s.SendToSession(new RemoveBuddyReply(m_removal_id));

            DatabaseManager.removeFriendBuddy(s, m_removal_id);
        }
    }
}
