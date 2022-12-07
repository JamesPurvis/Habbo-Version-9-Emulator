using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emulator.Network.Codecs;

namespace Emulator.Network.Handler
{
    public class ServerChannelIntializer : ChannelInitializer<IChannel>
    {
        protected override void InitChannel(IChannel channel)
        {
            IChannelPipeline m_pipeline = channel.Pipeline;
            m_pipeline.AddLast("Encoder", new ServerEncoder());
            m_pipeline.AddLast("Decoder", new ServerDecoder());
            m_pipeline.AddLast("Handler", new ServerChannelHandler());
        }
    }
}
