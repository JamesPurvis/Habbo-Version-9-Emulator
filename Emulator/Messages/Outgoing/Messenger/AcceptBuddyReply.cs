using Emulator.Game.Database;
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
    public class AcceptBuddyReply : MessageComposer
    {

        private int m_id;
        
        public AcceptBuddyReply(int id)
        {
            this.m_id = id;
       
        }
        public void compose(HabboResponse response)
        {
            UserModel m_user = DatabaseManager.returnEntityById(m_id);

            response.writeInt(m_user.user_id);
            response.writeString(m_user.user_name);
            if (m_user.user_gender == 'M') response.writeInt(1);
            if (m_user.user_gender == 'F') response.writeInt(0);
            response.writeString(m_user.user_console_mission);
            if (Startup.return_environment().return_session_manager().checkIfConnected(m_user.user_name)) response.writeString("I" + "Online");
            if (!Startup.return_environment().return_session_manager().checkIfConnected(m_user.user_name)) response.writeString("H" + "Offline");
            response.writeString(m_user.user_last_visited);
            response.writeString(m_user.user_figure);

            


        }

        public short return_header_id()
        {
            return 137;
        }
    }
}
