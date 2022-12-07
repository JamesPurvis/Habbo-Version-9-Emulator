using Emulator.Game.Database;
using Emulator.Game.Messenger;
using Emulator.Game.Models;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Outgoing.Messenger
{
    public class MessengerInitReply : MessageComposer
    {
        private GameSession m_session = null;

       public MessengerInitReply(GameSession s)
      {
           this.m_session = s;
      }
        public void compose(HabboResponse response)
        {
            response.writeString(m_session.returnUser.user_console_mission); //Persistent MSG

            response.writeInt(500); //User limit
            response.writeInt(100); //Normal Limit
            response.writeInt(300); //Extended Limit
            response.writeInt(m_session.return_messenger.return_friends.Count); //total friend count

            foreach(MessengerFriend friend in m_session.return_messenger.return_friends)
            {
                response.writeInt(friend.return_user_id);
                response.writeString(friend.return_user_name);
                response.writeInt(friend.return_user_gender);
                response.writeString(friend.return_console_status);
                friend.parse_activity(response);
                response.writeString(friend.return_last_visited);
                response.writeString(friend.return_user_figure);
            }
            
        }

        public short return_header_id()
        {
            return 12;
        }
    }
}
