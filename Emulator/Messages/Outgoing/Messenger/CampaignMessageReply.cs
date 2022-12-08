using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Outgoing.Messenger
{
    public class CampaignMessageReply : MessageComposer
    {
        public void compose(HabboResponse response)
        {
            response.writeInt(1);
            response.writeString("http://google.com/");
            response.writeString("Click here");
            response.writeString("Wow! Cool!");
        }

        public short return_header_id()
        {
            return 133;
        }
    }
}
