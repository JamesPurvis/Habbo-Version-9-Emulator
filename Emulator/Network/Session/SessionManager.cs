using Emulator.Game.Models;
using NHibernate.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Network.Session
{
    public class SessionManager
    {
        private LinkedHashMap<String, GameSession> m_active_clients;
        public SessionManager()
        {
            m_active_clients = new LinkedHashMap<String, GameSession>();
        }

        public void mapNewSession(String user_name, GameSession s)
        {
            m_active_clients.Add(user_name, s);
        }

        public GameSession returnGameSession(String user_name )
        {
            return m_active_clients[user_name];
        }

        public void removeMapping(String user_name)
        {
            m_active_clients.Remove(user_name);
        }

        public Boolean checkIfConnected(String user_name)
        {
           return m_active_clients.ContainsKey(user_name.ToLower());
        }
    }
}
