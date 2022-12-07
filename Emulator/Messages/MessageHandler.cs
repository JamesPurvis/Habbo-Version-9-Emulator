using DotNetty.Transport.Channels;
using Emulator.Messages.Incoming;
using Emulator.Messages.Incoming.Login;
using Emulator.Messages.Incoming.Register;
using Emulator.Messages.Incoming.Global;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emulator.Messages.Outgoing.Login;
using Emulator.Messages.Incoming.Navigator;
using Emulator.Messages.Incoming.Purse;
using Emulator.Utils;
using Emulator.Messages.Outgoing.Global;
using Emulator.Messages.Outgoing.Navigator;
using Emulator.Messages.Incoming.Messenger;
using Google.Protobuf.Reflection;

namespace Emulator.Messages
{
    public class MessageHandler
    {
        private MessageEvent[] m_packet_handlers = null;

        public MessageHandler()
        {
            m_packet_handlers = new MessageEvent[300];

            m_packet_handlers[202] = new GENERATEKEY();
            m_packet_handlers[49] = new GDATE();
            m_packet_handlers[42] = new APPROVENAME();
            m_packet_handlers[43] = new REGISTER();
            m_packet_handlers[197] = new APPROVE_EMAIL();
            m_packet_handlers[203] = new APPROVE_PASSWORD();
            m_packet_handlers[8] = new GET_CREDITS();
            m_packet_handlers[4] = new TRY_LOGIN();
            m_packet_handlers[150] = new NAVIGATE();
            m_packet_handlers[16] = new SUSERF();
            m_packet_handlers[12] = new MESSENGER_INIT();
            m_packet_handlers[15] = new MESSENGER_UPDATE();
            m_packet_handlers[41] = new MESSENGER_FIND_USER();
            m_packet_handlers[7] = new GET_INFO();
            m_packet_handlers[36] = new MESSENGER_ASSIGN_PER_MSG();
            m_packet_handlers[39] = new MESSENGER_REQUEST_BUDDY();
            m_packet_handlers[37] = new MESSENGER_ACCEPTBUDDY();
            m_packet_handlers[40] = new MESSENGER_REMOVE_BUDDY();
            m_packet_handlers[38] = new MESSENGER_DECLINE_BUDDY();
            m_packet_handlers[33] = new MESSENGER_SEND_MSG();

        }

        public void invokeEvent(HabboRequest r, GameSession s)
        {
            try
            {
                m_packet_handlers[r.return_header_id()].invokeEvent(r, s);
            }
            catch(NullReferenceException)
            {
                Logging.Logging.m_Logger.Error("Unable to find header " + r.return_header_id());
                return;
            }
            Logging.Logging.m_Logger.Info("Invoked event: " + r.return_header_id());
        }
    }
}
