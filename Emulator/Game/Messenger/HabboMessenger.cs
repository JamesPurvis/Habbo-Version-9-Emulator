using Emulator.Game.Database;
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

         public List<MessengerFriend> return_friends
        {
            get { return m_friends; }
        }
        public HabboMessenger(GameSession s)
        {
            m_session = s;
            m_session.return_messenger = this;
            m_friends = DatabaseManager.returnFriendsIDs(s);
        }
    }
}
