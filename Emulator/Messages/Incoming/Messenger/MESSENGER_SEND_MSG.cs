using Emulator.Game.Database;
using Emulator.Game.Models;
using Emulator.Messages.Outgoing.Messenger;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Incoming.Messenger
{
    public class MESSENGER_SEND_MSG : MessageEvent
    {

        public void invokeEvent(HabboRequest r, GameSession s)
        {
            int m_amount = r.popInteger();
            List<int> m_receiver_ids = new List<int>();

            while (m_amount > 0)
            {

                int m_receiver_id = r.popInteger();
                m_receiver_ids.Add(m_receiver_id);

                m_amount--;
            }

            string m_msg = r.popString();

            foreach (int i in m_receiver_ids)
            {
                MessengerMessages m_message = DatabaseManager.createNewMessage(i, s.returnUser.user_id, m_msg, DateTime.Now);
                String m_user_name = DatabaseManager.returnEntityById(i).user_name;

                if (Startup.return_environment().return_session_manager().checkIfConnected(m_user_name))
                {
                    Startup.return_environment().return_session_manager().returnGameSession(m_user_name).SendToSession(new MessengerMessagesReply(m_message.message_id, s.returnUser.user_id, DateTime.Now, m_msg));
                }

            }
        }
    }
}
