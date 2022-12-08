using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Emulator.Messages.Incoming.Messenger;
using Emulator.Network.Session;
using Emulator.Network.Streams;

namespace Emulator.Network.Codecs
{
    public class ServerDecoder : ByteToMessageDecoder
    {

        GameSession m_session = null;
        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
           if (input.ReadableBytes < 5)
            {
                return;
            }

            input.MarkReaderIndex();

            int m_packet_length = Emulator.Utils.Base64Encoding.decode(new byte[] {input.ReadByte(), input.ReadByte(), input.ReadByte()});


           if (input.ReadableBytes < m_packet_length)
            {
                input.ResetReaderIndex();
                return;
            }

           if (m_packet_length < 0)
            {
                return;
            }

            HabboRequest m_request = new HabboRequest(input.ReadBytes(m_packet_length), m_packet_length);

            if (m_session == null)
            {
                m_session = new GameSession(context);
            }

            Startup.return_environment().return_message_handler().invokeEvent(m_request, m_session);

            output.Add(m_request);

        }
    }
}
