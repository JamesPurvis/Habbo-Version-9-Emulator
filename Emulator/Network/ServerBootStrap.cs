using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Emulator.Logging;
using Emulator.Network.Handler;

namespace Emulator.Network
{
    public class ServerBootStrap
    {
        MultithreadEventLoopGroup m_boss_group;
        MultithreadEventLoopGroup m_worker_group;
        ServerBootstrap m_bootstrap;

        public ServerBootStrap()
        {
            m_boss_group = new MultithreadEventLoopGroup(1);
            m_worker_group = new MultithreadEventLoopGroup();
        }


        public void beginListening(String addr, int port)
        {
            try
            {
                m_bootstrap = new ServerBootstrap()
                     .Group(m_boss_group, m_worker_group)
                        .Channel<TcpServerSocketChannel>()
                        .ChildHandler(new ServerChannelIntializer())
                        .ChildOption(ChannelOption.TcpNodelay, true)
                        .ChildOption(ChannelOption.SoKeepalive, true)
                        .ChildOption(ChannelOption.SoReuseaddr, true)
                        .ChildOption(ChannelOption.SoRcvbuf, 1024)
                        .ChildOption(ChannelOption.RcvbufAllocator, new FixedRecvByteBufAllocator(1024))
                        .ChildOption(ChannelOption.Allocator, UnpooledByteBufferAllocator.Default);


                m_bootstrap.BindAsync(IPAddress.Parse(addr), port);
            }
            catch(Exception e)
            {
                Logging.Logging.m_Logger.Error(e.Message);
            }

            finally
            {
                Logging.Logging.m_Logger.Info("The server has started listening on " + addr + ":" + port);
            }
        }
    }
}
