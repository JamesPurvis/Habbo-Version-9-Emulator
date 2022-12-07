using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emulator.Game.Models;
using NHibernate;
using Emulator.Game.Database;

namespace Emulator.Messages.Outgoing.Login
{
    public class UserObjectReply : MessageComposer
    {
        private UserModel m_user;
        public UserObjectReply(UserModel user)
        {
            m_user = user;
        }
        public void compose(HabboResponse response)
        {
            response.write("name=" + m_user.user_name + Convert.ToChar(13));
            response.write("figure=" + m_user.user_figure + Convert.ToChar(13));
            response.write("sex=" + m_user.user_gender.ToString() + Convert.ToChar(13));
            response.write("customData=" + m_user.user_mission + Convert.ToChar(13));
            response.write("ph_tickets=" + 0 + Convert.ToChar(13));
            response.write("photo_film=" + 0  + Convert.ToChar(13));
            response.write("ph_figure=" + "" + Convert.ToChar(13));
            response.write("directmail=0" + Convert.ToChar(13));

            try
            {
                m_user.user_last_visited = DateTime.Now;

                using (ISession m_session = DatabaseManager.openSession())
                {
                    m_session.Update(m_user);
                    m_session.Flush();
                    m_session.Close();
                }
            }
            catch (Exception e)
            {
                Logging.Logging.m_Logger.Error(e.Message);
            }
        }

        public short return_header_id()
        {
            return 5;
        }
    }
}
