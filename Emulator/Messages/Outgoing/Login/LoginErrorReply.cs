using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Outgoing.Login
{
    public class LoginErrorReply : MessageComposer
    {
        public void compose(HabboResponse response)
        {
            response.writeString("login incorrect");
        }

        public short return_header_id()
        {
            return 33;
        }
    }
}
