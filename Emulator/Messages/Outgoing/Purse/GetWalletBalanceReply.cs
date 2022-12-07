using Emulator.Network.Streams;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emulator.Network.Session;

namespace Emulator.Messages.Outgoing.Purse
{
    public class GetWalletBalanceReply : MessageComposer
    {
        private GameSession m_current_session;
        public GetWalletBalanceReply(GameSession s)
        {
            m_current_session = s;
        }
        public void compose(HabboResponse response)
        {
            response.writeString(m_current_session.returnUser.user_credits);
            response.writeString(".0");
        }

        public short return_header_id()
        {
           return 6;
        }
    }
}
