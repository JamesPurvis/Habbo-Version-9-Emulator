using Emulator.Network.Session;
using Emulator.Network.Streams;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Outgoing.Room.Users
{
    public class UserStatusReply : MessageComposer
    {
        private int goal_x;
        private int goal_y;
        private string head_rotate;
        private string body_rotate;
        private string stat;
        private GameSession m_session;
        private string poststat;

        public UserStatusReply(String stat, GameSession s, int goal_x, int goal_y, string headrotate, string bodyrotate, string poststat)
        {
            this.stat = stat;
            this.m_session = s;
            this.goal_x = goal_x;
            this.goal_y = goal_y;
            this.head_rotate = headrotate;
            this.body_rotate= bodyrotate;
            this.poststat = poststat;
        }

        public void compose(HabboResponse response)
        {
            response.write(m_session.returnUser.user_id);
            response.write(" ");
            response.write(m_session.return_room_user.m_current_x);
            response.write(",");
            response.write(m_session.return_room_user.m_current_y);
            response.write(",");
            response.write("0.0");
            response.write(",");
            response.write(head_rotate);
            response.write(",");
            response.write(body_rotate);
            response.write(poststat);
            
        }

        public short return_header_id()
        {
            return 34;
        }
    }
}
