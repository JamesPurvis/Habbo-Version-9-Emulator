using DotNetty.Buffers;
using Emulator.Game.Messenger;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using Emulator.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Outgoing.Messenger
{
    public class BuddyListReply : MessageComposer
    {
        private GameSession m_session = null;       

        public BuddyListReply(GameSession session)
        {
            m_session = session;
        }
     
        public void compose(HabboResponse response)
        {
            response.writeInt(m_session.return_messenger.return_friends.Count);

            foreach (MessengerFriend m_friend in m_session.return_messenger.return_friends)
            {
                response.writeInt(m_friend.return_user_id);
                response.writeString(m_friend.return_user_name);
                response.writeInt(m_friend.return_user_gender);
                response.writeString(m_friend.return_console_status);
                m_friend.parse_activity(response);
                response.writeString(m_friend.return_last_visited);
                response.writeString(m_friend.return_user_figure);
            }
            
            
            
        }

        public short return_header_id()
        {
            return 263;
        }
    }
}

