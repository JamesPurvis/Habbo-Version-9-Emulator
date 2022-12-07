using Emulator.Game.Models;
using Emulator.Network.Session;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Emulator.Messages.Incoming.Login
{
    public static class LoginAuthenticator
    {
        public static Boolean tryLogin(String userName, String password, GameSession s)
        {
            return s.return_database_session.QueryOver<UserModel>().Where(x => x.user_name == userName && x.user_password == password).RowCount() > 0;

        }
    }
}
