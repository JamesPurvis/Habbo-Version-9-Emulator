using Emulator.Game.Messenger;
using Emulator.Messages.Outgoing.Messenger;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Incoming.Messenger
{
    public class MESSENGER_UPDATE : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            s.return_messenger.UpdateFriendsList();
             s.SendToSession(new MessengerUpdateReply(s));
        }
    }
}
