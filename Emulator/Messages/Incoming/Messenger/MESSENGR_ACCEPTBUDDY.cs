using Emulator.Game.Database;
using Emulator.Game.Models;
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
    //TODO: When a messenger request is accepted the request isn't deleted for some reason.
    public class MESSENGER_ACCEPTBUDDY : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            int m_id = r.popInteger();

            UserModel m_model = DatabaseManager.returnEntityById(m_id);

            if (Startup.return_environment().return_session_manager().checkIfConnected(m_model.user_name))
            {
                Startup.return_environment().return_session_manager().returnGameSession(m_model.user_name).SendToSession(new AcceptBuddyReply(s.returnUser.user_id));
            }


            s.SendToSession(new AcceptBuddyReply(m_id));

            DatabaseManager.saveFriendBuddy(s, m_id);
            DatabaseManager.removeFriendRequest(m_id, s.returnUser.user_id);

            if (DatabaseManager.return_friend_request(s.returnUser.user_id, m_id) !!= null)
            {
                DatabaseManager.removeFriendRequest(s.returnUser.user_id, m_id);
            }
        }


    }
}
