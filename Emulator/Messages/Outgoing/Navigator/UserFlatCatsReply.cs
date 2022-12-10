using Emulator.Game.Database;
using Emulator.Game.Models;
using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Outgoing.Navigator
{
    public class UserFlatCatsReply : MessageComposer
    {
        public void compose(HabboResponse response)
        {
           IList<NavigatorCategory> m_flat_cats = DatabaseManager.return_user_flat_cats();
            
            response.writeInt(m_flat_cats.Count);
            response.writeInt(0);
            response.writeString("No category");

            foreach(NavigatorCategory category in m_flat_cats)
            {
                if (category.category_parent_id != 0)
                {
                    response.writeInt(category.category_id);
                    response.writeString(category.category_name);
                }
            }
        }

        public short return_header_id()
        {
            return 221;
        }
    }
}
