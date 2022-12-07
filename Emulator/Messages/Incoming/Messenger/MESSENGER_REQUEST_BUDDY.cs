using Emulator.Game.Database;
using Emulator.Messages.Outgoing.Global;
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
    public class MESSENGER_REQUEST_BUDDY : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            String m_user_name = r.popString();

            int m_user_id = DatabaseManager.returnUserIdByName(m_user_name);

            if (!DatabaseManager.FriendRequestsExists(m_user_id, s.returnUser.user_id))
            {
                DatabaseManager.createNewFriendRequest(s, m_user_id);

                if (Startup.return_environment().return_session_manager().checkIfConnected(m_user_name))
                {
                    Startup.return_environment().return_session_manager().returnGameSession(m_user_name).SendToSession(new BuddyRequestReply(s.returnUser.user_id, s.returnUser.user_name));
                }
            }
            else
            {
                s.SendToSession(new HotelAlertReply("Stalker alert, you already sent a request!"));
            }
        }
    }
}
