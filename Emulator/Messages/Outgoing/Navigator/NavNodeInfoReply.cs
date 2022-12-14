using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emulator.Game.Database;
using Emulator.Game.Models;
using Emulator.Utils;
using Emulator.Network.Session;

namespace Emulator.Messages.Outgoing.Navigator
{
    public class NavNodeInfoReply : MessageComposer
    {

        private int m_category_id;
        private int m_show_full;
        private NavigatorCategory m_category;
        private GameSession m_session;
        public NavNodeInfoReply(int show_full, int category_id, GameSession s)
        {
            m_category_id = category_id;
            m_show_full = show_full;
            m_session = s;
            m_category = Startup.return_environment().return_navigator_manager().return_category_instance(m_category_id);
        }

        public void compose(HabboResponse response)
        {
            response.writeInt(m_show_full);
            response.writeInt(m_category_id);
            response.writeInt(m_category.category_type);
            response.writeString(m_category.category_name);
            response.writeInt(0);
            response.writeInt(10000);
            response.writeInt(m_category.category_parent_id);

            if (m_category.category_type == 2)
            {
                response.writeInt(m_category.return_room_count());
            }

              if (m_category.category_type == 2)
              {
                  compile_children_rooms(response);
              }

             if (m_category.category_type == 0)
             {
                 compile_public_rooms(response);
             }

            compile_children_nodes(response);

        }


        private void compile_children_nodes(HabboResponse response)
        {

            foreach (NavigatorCategory child in m_category.return_sub_categories())
            {
                response.writeInt(child.category_id);
                response.writeInt(0);
                response.writeString(child.category_name);
                response.writeInt(0);
                response.writeInt(100);
                response.writeInt(child.category_parent_id);
            }

        }

        private void compile_children_rooms(HabboResponse response)
        {

            foreach (NavigatorRooms child in m_category.return_child_rooms()) 
            {
               response.writeInt(child.room_id);
               response.writeString(child.room_name);
               response.writeString(child.room_owner);
               response.writeString(child.room_status);
               response.writeInt(child.room_visitors);
               response.writeInt(child.room_max_visitors);
               response.writeString(child.room_description);
            }
        }

        private void compile_public_rooms(HabboResponse response)
        {

            foreach (NavigatorRooms child in m_category.return_child_rooms())
            {
                response.writeInt(child.room_id + 1000);
                response.writeInt(1);
                response.writeString(child.room_name);
                response.writeInt(child.room_visitors);
                response.writeInt(child.room_max_visitors);
                response.writeInt(child.room_category_id);
                response.writeString(child.room_description);
                response.writeInt(child.room_id);
                response.writeInt(0);
                response.writeString(child.room_cct);
                response.writeInt(0);
                response.writeInt(1);

            }
        }


        public short return_header_id()
        {
            return 220;
        }
    }
}
