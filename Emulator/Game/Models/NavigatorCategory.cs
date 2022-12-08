using Emulator.Game.Database;
using NHibernate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Game.Models
{
    public class NavigatorCategory
    {
        public virtual int category_id { get; set; }
        public virtual string category_name { get; set; }

        public virtual int category_type { get; set; }

        public virtual int category_parent_id { get; set; }

        public virtual IList<NavigatorCategory> return_sub_categories()
        {
            IList<NavigatorCategory> sub_categories_list;

            using (ISession m_session = DatabaseManager.openSession())
            {
                sub_categories_list = m_session.QueryOver<NavigatorCategory>().Where(x => x.category_parent_id == category_id).List();
                m_session.Close();
            }

            return sub_categories_list;
        }

        public virtual int return_room_count()
        {
            int m_count;

            using (ISession m_session = DatabaseManager.openSession())
            {
                m_count = m_session.QueryOver<NavigatorPrivates>().Where(x => x.room_category_id == category_id).RowCount();
                m_session.Close();
            }

            return m_count;


        }

        public virtual IList return_child_rooms()
        {
            IList m_room_list;

            using (ISession m_session = DatabaseManager.openSession())
            {
                if (category_type == 0)
                {
                    m_room_list = (IList)m_session.QueryOver<NavigatorPublics>().Where(x => x.room_category_id == category_id).List();
                }
                else
                {
                    m_room_list = (IList)m_session.QueryOver<NavigatorPrivates>().Where(x => x.room_category_id == category_id).List();
                }

                m_session.Close();
            }

            return m_room_list;
        }
    }
}
