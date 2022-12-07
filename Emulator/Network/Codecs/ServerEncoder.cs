using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Emulator.Messages;
using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Network.Codecs
{
    public class ServerEncoder : MessageToMessageEncoder<MessageComposer>
    {
        protected override void Encode(IChannelHandlerContext context, MessageComposer m, List<object> output)
        {
            IByteBuffer m_buffer = context.Allocator.Buffer();


            HabboResponse m_response = new HabboResponse(m.return_header_id(), m_buffer);
            try
            {
                m.compose(m_response);
            }
            catch(Exception e)
            {
                Logging.Logging.m_Logger.Info(e.Message);
            }

            m_buffer.WriteByte(1);

            Logging.Logging.m_Logger.Debug("SENT: " + m_response.returnString());

            output.Add(m_buffer);
        }
    }
}
