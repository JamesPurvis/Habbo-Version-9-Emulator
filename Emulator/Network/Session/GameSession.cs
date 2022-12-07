﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Emulator.Game.Messenger;
using Emulator.Game.Models;
using Emulator.Network.Streams;
using NHibernate;
using Org.BouncyCastle.Crypto.Signers;

namespace Emulator.Network.Session
{
    public class GameSession
    {

        private IChannel m_channel;
        private UserModel m_user;
        private ISession m_database_session;
        private HabboMessenger m_messenger;
        public IChannel returnChannel
        {
            get {  return m_channel; }
        }

        public UserModel returnUser
        {
           set { m_user = value; }
            get { return m_user;  }
        }

        public ISession return_database_session
        {
            get { return m_database_session; }
        }

        public HabboMessenger return_messenger
        {
            set { m_messenger = value;  }
            get { return m_messenger; }
        }

        public GameSession(IChannelHandlerContext c)
        {
            m_channel = c.Channel;
            m_database_session = Emulator.Game.Database.DatabaseManager.openSession();
        }

        public void SendToSession(Object o)
        {
            m_channel.WriteAndFlushAsync(o);
        }
    }
}
