using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emulator.Game.Rooms;
using Emulator.Game.Models;
using Emulator.Network.Session;
using Emulator.Game.Database;

namespace Emulator.Messages.Outgoing.Room
{
    public class UsersReply : MessageComposer
    {
        private Emulator.Game.Rooms.Room m_instance;
        private GameSession m_session;
        public UsersReply(Emulator.Game.Rooms.Room instance, GameSession session)
        {
            this.m_instance = instance;
            this.m_session = session;
        }
        public void compose(HabboResponse response)
        {
           foreach(RoomUser user in m_instance.return_room_users)
            {
                response.write("i:" + user.m_user_model.user_id + Convert.ToChar(13));
                response.write("n:" + user.m_user_model.user_name + Convert.ToChar(13));
                response.write("f:" + user.m_user_model.user_figure + Convert.ToChar(13));
                response.write("l:" + DatabaseManager.return_door(m_instance) + Convert.ToChar(13));
                response.write("s:" + user.m_user_model.user_gender + Convert.ToChar(13));
                response.write("c:" + user.m_user_model.user_mission + Convert.ToChar(13));
            }
        }

        public short return_header_id()
        {
            return 28;
        }
    }
}
