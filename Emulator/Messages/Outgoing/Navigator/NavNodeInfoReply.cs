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
            m_category = DatabaseManager.returnEntity(m_category_id, s);
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
                response.writeInt(DatabaseManager.openSession().QueryOver<NavigatorPrivates>().Where(x => x.room_category_id == m_category_id).List().Count);
            }



            if (m_category.category_type == 2)
            {
                response.write(compile_children_rooms().ToString());
            }

            if (m_category.category_type == 0)
            {
                response.write(compile_public_rooms().ToString());
            }

            response.write(compile_children_nodes().ToString());

        }


        private StringBuilder compile_children_nodes()
        {
            IList<NavigatorCategory> m_children = DatabaseManager.openSession().QueryOver<NavigatorCategory>().Where(x => x.category_parent_id == m_category_id).List();
            StringBuilder m_builder = new StringBuilder();

            foreach (NavigatorCategory child in m_children)
            {
                m_builder.Append(Encoding.GetEncoding("ISO-8859-1").GetString(VL64Encoding.encode(child.category_id)));
                m_builder.Append(Encoding.GetEncoding("ISO-8859-1").GetString(VL64Encoding.encode(0)));
                m_builder.Append(child.category_name);
                m_builder.Append((char)2);
                m_builder.Append(Encoding.GetEncoding("ISO-8859-1").GetString(VL64Encoding.encode(0)));
                m_builder.Append(Encoding.GetEncoding("ISO-8859-1").GetString(VL64Encoding.encode(100)));
                m_builder.Append(Encoding.GetEncoding("ISO-8859-1").GetString(VL64Encoding.encode(child.category_parent_id)));
            }

            return m_builder;

        }

        private StringBuilder compile_children_rooms()
        {
             StringBuilder m_builder = new StringBuilder();
            IList<NavigatorPrivates> m_children = DatabaseManager.openSession().QueryOver<NavigatorPrivates>().Where(x => x.room_category_id == m_category_id).List();

            foreach(NavigatorPrivates child in m_children)
            {
                m_builder.Append(Encoding.GetEncoding("ISO-8859-1").GetString(VL64Encoding.encode(child.room_id)));
                m_builder.Append(child.room_name);
                m_builder.Append((char)2);
                m_builder.Append(child.room_owner);
                m_builder.Append((char)2);
                m_builder.Append(child.room_status);
                m_builder.Append((char)2);
                m_builder.Append(Encoding.GetEncoding("ISO-8859-1").GetString(VL64Encoding.encode(child.room_visitors)));
                m_builder.Append(Encoding.GetEncoding("ISO-8859-1").GetString(VL64Encoding.encode(child.room_max_visitors)));
                m_builder.Append(child.room_description);
                m_builder.Append((char)2);


            }

            return m_builder;
        }

        private StringBuilder compile_public_rooms()
        {
            StringBuilder m_builder = new StringBuilder();
            IList<NavigatorPublics> m_children = DatabaseManager.openSession().QueryOver<NavigatorPublics>().Where(x => x.room_category_id == m_category_id).List();

            foreach(NavigatorPublics child in m_children)
            {
                m_builder.Append(Encoding.GetEncoding("ISO-8859-1").GetString(VL64Encoding.encode(child.room_id)));
                m_builder.Append(Encoding.GetEncoding("ISO-8859-1").GetString(VL64Encoding.encode(1)));
                m_builder.Append(child.room_name);
                m_builder.Append((char)2);
                m_builder.Append(Encoding.GetEncoding("ISO-8859-1").GetString(VL64Encoding.encode(child.room_current_visitors)));
                m_builder.Append(Encoding.GetEncoding("ISO-8859-1").GetString(VL64Encoding.encode(child.room_max_visitors)));
                m_builder.Append(Encoding.GetEncoding("ISO-8859-1").GetString(VL64Encoding.encode(child.room_category_id)));
                m_builder.Append(child.room_description);
                m_builder.Append((char)2);
                m_builder.Append(Encoding.GetEncoding("ISO-8859-1").GetString(VL64Encoding.encode(child.room_id)));
                m_builder.Append(Encoding.GetEncoding("ISO-8859-1").GetString(VL64Encoding.encode(0)));
                m_builder.Append(child.room_cct);
                m_builder.Append((char)2);
                m_builder.Append(Encoding.GetEncoding("ISO-8859-1").GetString(VL64Encoding.encode(0)));
                m_builder.Append(Encoding.GetEncoding("ISO-8859-1").GetString(VL64Encoding.encode(1)));
            }

            return m_builder;
        }


        public short return_header_id()
        {
            return 220;
        }
    }
}
