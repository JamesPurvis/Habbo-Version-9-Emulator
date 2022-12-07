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
            m_packet_handlers[12] = new MESSENGERINIT();
            m_packet_handlers[15] = new MESSENGERUPDATE();

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
