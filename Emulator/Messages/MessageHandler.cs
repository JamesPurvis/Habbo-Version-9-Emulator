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
using Emulator.Messages.Incoming.Rooms;
using Emulator.Messages.Outgoing.Room;
using Emulator.Messages.Incoming.Rooms.Users;
using Emulator.Messages.Incoming.Catalog;

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
            m_packet_handlers[32] = new MESSENGER_MARKREAD();
            m_packet_handlers[151] = new GETUSERFLATCATS();
            m_packet_handlers[29] = new CREATEFLAT();
            m_packet_handlers[25] = new SETFLATINFO();
            m_packet_handlers[153] = new SETFLATCAT();
            m_packet_handlers[17] = new SRCHF();
            m_packet_handlers[19] = new ADD_FAVORITE_ROOM();
            m_packet_handlers[20] = new DELETE_FAVORITE_ROOM();
            m_packet_handlers[18] = new GETFVRF();
            m_packet_handlers[21] = new GETFLATINFO();
            m_packet_handlers[152] = new GETFLATCAT();
            m_packet_handlers[24] = new UPDATEFLAT();
            m_packet_handlers[44] = new UPDATE();
            m_packet_handlers[182] = new GETINTEREST();
            m_packet_handlers[2] = new ROOMDIRECTORY();
            m_packet_handlers[57] = new TRYFLAT();
            m_packet_handlers[59] = new GOTOFLAT();
            m_packet_handlers[60] = new G_HMAP();
            m_packet_handlers[62] = new G_OBJS();
            m_packet_handlers[63] = new G_ITEMS();
            m_packet_handlers[61] = new G_USERS();
            m_packet_handlers[126] = new GETROOMAD();
            m_packet_handlers[52] = new CHAT();
            m_packet_handlers[55] = new SHOUT();
            m_packet_handlers[56] = new WHISPER();
            m_packet_handlers[75] = new WALK();
            m_packet_handlers[101] = new GCIX();
            m_packet_handlers[102] = new GCAP();


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
