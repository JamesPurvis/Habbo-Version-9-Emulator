using Emulator.Game.Database;
using Emulator.Game.Models;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Outgoing.Register
{
    public class ApproveNameReply : MessageComposer
    {

        private string m_input_name;
        private GameSession m_session;
        public ApproveNameReply(string name, GameSession s)
        {
            m_input_name = name;
            m_session = s;
        }
        public void compose(HabboResponse response)
        {
            if (!DatabaseManager.checkIfUserExists(m_input_name))
            {
                response.writeInt(0);
            }
            else
            {
                response.writeInt(4);
            }
        }

        public short return_header_id()
        {
            return 36;
        }
    }
}
