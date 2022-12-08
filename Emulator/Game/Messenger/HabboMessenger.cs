using Emulator.Game.Database;
using Emulator.Game.Models;
using Emulator.Messages.Outgoing.Messenger;
using Emulator.Network.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Game.Messenger
{
    public class HabboMessenger
    {
        private GameSession m_session;
        private List<MessengerFriend> m_friends;
        private IList<MessengerRequests> m_requests;
        private IList<MessengerMessages> m_messages;
         public List<MessengerFriend> return_friends
        {
            get { return m_friends; }
            set { m_friends = value;  }
        }

        public IList<MessengerRequests> return_requests
        {
            get { return m_requests;  }
        }

        public IList<MessengerMessages> return_messages
        {
            get { return m_messages; }
        }
        public HabboMessenger(GameSession s)
        {
            m_session = s;
            m_session.return_messenger = this;
            m_friends = DatabaseManager.returnFriendsIDs(m_session.returnUser.user_id);
            m_requests = DatabaseManager.returnAllFriendRequests(m_session.returnUser.user_id);
            m_messages = DatabaseManager.returnAllMessages(s.returnUser.user_id);
        }

        public void SendLoginFriendRequests()
        {
            foreach(MessengerRequests request in m_requests)
            {
                m_session.SendToSession(new BuddyRequestReply(request.from_id, DatabaseManager.returnEntityById(request.from_id).user_name));
            }
        }

        public void UpdateFriendsList()
        {
            m_friends = DatabaseManager.returnFriendsIDs(m_session.returnUser.user_id);
        }

        public void SendLoginMessages()
        {
            foreach(MessengerMessages message in m_messages)
            {
                m_session.SendToSession(new MessengerMessagesReply(message.message_id, message.sender_id, message.time_sent, message.message_text));
            }
        }
    }
}
