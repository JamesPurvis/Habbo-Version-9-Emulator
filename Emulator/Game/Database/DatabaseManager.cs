using Emulator.Game.Messenger;
using Emulator.Game.Models;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Dialect;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Game.Database
{
    public class DatabaseManager
    {

        private static ISessionFactory m_session_factory;

        public DatabaseManager()
        {
            try
            {
                m_session_factory = Fluently.Configure()
               .Database(MySQLConfiguration.Standard.ConnectionString("Server=localhost;Database=habboserver;Uid=root;Pwd=;").ShowSql())
              .Mappings(m => m.FluentMappings
             .AddFromAssemblyOf<Environment>())
             .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true))
             .BuildSessionFactory();

            }
            catch(Exception e)
            {
                Logging.Logging.m_Logger.Error(e.Message);
            }
            
            {
                Logging.Logging.m_Logger.Debug("Connected to mySQL database successfully.");
            }
        }


       public static ISession openSession()
       {
            
            return m_session_factory.OpenSession();
        }


        public static UserModel returnEntity(String userName, GameSession s)
        {
            UserModel m_user = s.return_database_session.Query<UserModel>().Where(x => x.user_name == userName).FirstOrDefault();

            Console.WriteLine(m_user.user_name);

            return m_user;

        }

        public static NavigatorCategory returnEntity(int category_id, GameSession s)
        {
            NavigatorCategory m_category = s.return_database_session.Query<NavigatorCategory>().Where(x => x.category_id == category_id).FirstOrDefault();

            return m_category;
        }

        public static IList<NavigatorPrivates> returnRoomByOwner(String owner, GameSession s)
        {
            return s.return_database_session.Query<NavigatorPrivates>().Where(x => x.room_owner == owner).ToList();

        }

        public static List<MessengerFriend> returnFriendsIDs(GameSession s)
        {
            //figure out how to do this in one query.

            List<MessengerFriend> m_friends = new List<MessengerFriend>();

            IList<MessengerFriends> m_friends_query = s.return_database_session.QueryOver<MessengerFriends>().Where(x => x.user_id == s.returnUser.user_id).List<MessengerFriends>();

            IList<MessengerFriends> m_friends_query2 = s.return_database_session.QueryOver<MessengerFriends>().Where(x => x.friend_id == s.returnUser.user_id).List<MessengerFriends>();





            foreach (MessengerFriends f in m_friends_query)
            {
                m_friends.Add(new MessengerFriend(f.friend_id, s));
                
            }

            foreach(MessengerFriends g in m_friends_query2)
            {
                m_friends.Add(new MessengerFriend(g.user_id, s));
            }

            return m_friends;
        }

        public static UserModel returnEntityById(int id, GameSession s)
        {
            return s.return_database_session.Query<UserModel>().Where(x => x.user_id == id).FirstOrDefault();
        }
    }
}