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
    public class MESSENGER_SEND_MSG : MessageEvent
    {
        //TODO: When you send a message, it is not saved into the DB, also delete needs to be implemented.
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            int m_amount = r.popInteger();

            while(m_amount > 0)
            {
                int m_receiver_id = r.popInteger();
                string m_msg = r.popString();

                String m_user_name = DatabaseManager.returnEntityById(m_receiver_id).user_name;

                if (Startup.return_environment().return_session_manager().checkIfConnected(m_user_name))
                {
                    Startup.return_environment().return_session_manager().returnGameSession(m_user_name).SendToSession(new MessengerMessagesReply(m_receiver_id, s.returnUser.user_id, DateTime.Now, m_msg));
                }

                m_amount--;
            }
        }
    }
}
