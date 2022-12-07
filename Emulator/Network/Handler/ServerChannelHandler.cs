using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using Emulator.Network.Session;

namespace Emulator.Network.Handler
{
    public class ServerChannelHandler : ChannelHandlerAdapter
    {
        public override void ChannelActive(IChannelHandlerContext context)
        {
            base.ChannelActive(context);

            context.Channel.WriteAndFlushAsync(Unpooled.CopiedBuffer(Encoding.GetEncoding("ISO-8859-1").GetBytes("@@" + (char)1)));
            Logging.Logging.m_Logger.Info("A client has connected to the server");
        }


        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            base.ChannelRead(context, message);
         
        }
    }
}
