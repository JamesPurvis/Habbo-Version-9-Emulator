using DotNetty.Transport.Channels;
using Emulator.Game.Models;
using Emulator.Messages.Outgoing;
using Emulator.Messages.Outgoing.Register;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using NHibernate.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Incoming.Register
{
    public class REGISTER : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            string m_to_string = r.toString();
            HandleUserRegistration(Utils.UserRegistrationParser.return_parsed_data(m_to_string), s);
            s.SendToSession(new RegisterOkReply());
        }


        public void HandleUserRegistration(LinkedHashMap<int, string> m_user_props, GameSession s)
        {
            string m_username = m_user_props[2];
            string m_password = m_user_props[3];
            string m_figure = m_user_props[4];
            char m_gender = char.Parse(m_user_props[5]);
            string m_email = m_user_props[7];
            string m_birthday = m_user_props[8];

            UserModel newUser = new UserModel();

            newUser.user_name = m_username;
            newUser.user_password = m_password;
            newUser.user_figure = m_figure;
            newUser.user_gender = m_gender;
            newUser.user_email = m_email;
            newUser.user_birthday = m_birthday;
            newUser.user_mission = "I love harbo hotel.";
            newUser.user_credits = 100;
            newUser.user_console_mission = "I love harbo hotel.";
            newUser.user_last_visited = DateTime.Now;

            s.return_database_session.Save(newUser);

           // Startup.return_environment().return_session_manager().mapNewSession(newUser.user_name, s);
        }
    }
}
