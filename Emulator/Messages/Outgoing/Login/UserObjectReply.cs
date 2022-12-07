using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emulator.Game.Models;

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
            //USER OBJECT on version 9 uses the old packet structure? Why was it not updated. This will work for now.
            StringBuilder mGetInfo = new StringBuilder();
            mGetInfo.Append("name=" + m_user.user_name + Convert.ToChar(13));
            mGetInfo.Append("figure=" + m_user.user_figure + Convert.ToChar(13));
            mGetInfo.Append("sex=" + m_user.user_gender.ToString() + Convert.ToChar(13));
            mGetInfo.Append("customData=" + m_user.user_mission + Convert.ToChar(13));
            mGetInfo.Append("ph_tickets=" + 0 + Convert.ToChar(13));
            mGetInfo.Append("photo_film=" + 0  + Convert.ToChar(13));
            mGetInfo.Append("ph_figure=" + "" + Convert.ToChar(13));
            mGetInfo.Append("directmail=0" + Convert.ToChar(13));

            response.write(mGetInfo.ToString());
        }

        public short return_header_id()
        {
            return 5;
        }
    }
}
