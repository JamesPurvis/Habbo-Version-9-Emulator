using Emulator.Game.Database;
using Emulator.Game.Models;
using Emulator.Network.Streams;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Outgoing.Messenger
{
    public class MessengerMessagesReply : MessageComposer
    {

        private int m_id;
        private int m_sender_id;
        private DateTime m_time_sent;
        private string m_msg;

        public MessengerMessagesReply(int id, int senderID, DateTime time, String msg)
        {
            this.m_id = id;
            this.m_sender_id = senderID;
            this.m_time_sent = time;
            this.m_msg = msg;
        }
        public void compose(HabboResponse response)
        {
            UserModel m_sender = DatabaseManager.returnEntityById(m_sender_id);

            response.writeInt(1);
            response.writeInt(m_id);
            response.writeInt(m_sender_id);
            if (m_sender.user_gender == 'M') response.writeInt(1);
            if(m_sender.user_gender == 'F') response.writeInt(0);
            response.writeString(m_sender.user_figure);
            response.writeString(m_time_sent.ToString());
            response.writeString(m_msg);

           

        }

        public short return_header_id()
        {
            return 134;
        }
    }
}
