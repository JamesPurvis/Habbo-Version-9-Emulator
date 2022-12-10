using Emulator.Game.Database;
using Emulator.Game.Models;
using NHibernate;
using NHibernate.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Game.Navigator
{
    public class NavigatorManager
    {
        private LinkedHashMap<int, NavigatorCategory> m_category_map;
        public NavigatorManager()
        {
            Logging.Logging.m_Logger.Debug("A new navigator manager has been instantiated successfully!");
        }

        public void LoadNavigator()
        {
            m_category_map = new LinkedHashMap<int, NavigatorCategory>();

            IList<NavigatorCategory> m_categories = return_all_categories();

            foreach(NavigatorCategory n in m_categories)
            {
                m_category_map.Add(n.category_id, n);
            }

            Logging.Logging.m_Logger.Debug(m_category_map.Count + " " + "categories have been loaded into memory!");
        }

        public IList<NavigatorCategory> return_all_categories()
        {
            IList<NavigatorCategory> m_navigator_categories;

            using (ISession m_session = DatabaseManager.openSession())
            {
                m_navigator_categories = m_session.CreateCriteria<NavigatorCategory>().List<NavigatorCategory>();
                m_session.Close();
            }

            return m_navigator_categories;
        }

        public NavigatorCategory return_category_instance(int id)
        {
            return m_category_map[id];
        }
    }
}
