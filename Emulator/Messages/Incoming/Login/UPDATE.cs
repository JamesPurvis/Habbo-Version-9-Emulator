using Emulator.Game.Database;
using Emulator.Game.Models;
using Emulator.Messages.Outgoing.Login;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using NHibernate.Mapping;
using NHibernate.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Incoming.Login
{
    public class UPDATE : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            String m_user_data = r.return_body();

            LinkedHashMap<int, string> m_map = Utils.UpdateAccountParser.return_props(m_user_data);

            foreach(int header in m_map.Keys)
            {
                switch(header)
                {
                    case 4:
                        s.returnUser.user_figure = m_map[header];
                        break;

                    case 5:
                        s.returnUser.user_gender = char.Parse(m_map[header]);
                        break;

                    case 6:
                        s.returnUser.user_mission = m_map[header];
                        break;
                }
            }

            DatabaseManager.updateUserAccount(s.returnUser);

            s.SendToSession(new UpdateOkReply());
        }
    }
}
