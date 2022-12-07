using DotNetty.Transport.Channels;
using Emulator.Messages.Outgoing.Login;
using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emulator.Game.Models;
using Emulator.Game.Database;
using Emulator.Network.Session;
using Emulator.Messages.Outgoing.Messenger;
using Emulator.Game.Messenger;

namespace Emulator.Messages.Incoming.Login
{
    public class TRY_LOGIN : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            String m_username = r.popString().ToLower();
            String m_password = r.popString().ToLower();

            if (LoginAuthenticator.tryLogin(m_username, m_password, s))
            {
                
               if (Startup.return_environment().return_session_manager().checkIfConnected(m_username))
               {
                   Logging.Logging.m_Logger.Debug("DISCONNECTED");
                  Startup.return_environment().return_session_manager().returnGameSession(m_username).returnChannel.CloseAsync();
                  Startup.return_environment().return_session_manager().removeMapping(m_username);
               }
               
                

                s.SendToSession(new LoginOkReply());
                UserModel m_current_user = DatabaseManager.returnEntity(m_username, s);
                Startup.return_environment().return_session_manager().mapNewSession(m_username, s);
                s.returnUser = m_current_user;
                s.SendToSession(new UserObjectReply(m_current_user));

                try
                {
                    m_current_user.user_last_visited = DateTime.Now;
                    s.return_database_session.Save(m_current_user);
                    s.return_database_session.Flush();
                }
                catch(Exception e)
                {
                    Logging.Logging.m_Logger.Error(e.Message);
                }


                HabboMessenger m_messenger = new HabboMessenger(s);
             }
            else
            {
                s.SendToSession(new LoginErrorReply());


            }
        }
    }
}
