using Emulator.Game.Messenger;
using Emulator.Game.Models;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MySqlX.XDevAPI.Common;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Dialect;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
               .Database(MySQLConfiguration.Standard.ConnectionString("Server=localhost;Database=habboserver;Uid=root;Pwd=;"))
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


        public static UserModel returnEntity(String userName)
        {
            UserModel m_user;

            using (ISession m_session = openSession())
            {
               m_user = m_session.Query<UserModel>().Where(x => x.user_name == userName).FirstOrDefault();
                m_session.Close();
            }
               

            return m_user;

        }

        public static NavigatorCategory returnEntity(int category_id)
        {
            NavigatorCategory m_category;
            using (ISession m_session = openSession())
            {
                m_category = m_session.Query<NavigatorCategory>().Where(x => x.category_id == category_id).FirstOrDefault();
                m_session.Close();
            }

            return m_category;
        }

        public static IList<NavigatorPrivates> returnRoomByOwner(String owner)
        {
            IList<NavigatorPrivates> m_list = null;

            using (ISession m_session = openSession())
            {
                m_list = m_session.Query<NavigatorPrivates>().Where(x => x.room_owner == owner).ToList();

                m_session.Close();
            }

            return m_list;
            

        }

        public static List<MessengerFriend> returnFriendsIDs(int userid)
        {
            List<MessengerFriend> m_friends = new List<MessengerFriend>();

            using (ISession m_session = openSession())
            {

                IList<MessengerFriends> m_friends_query = m_session.QueryOver<MessengerFriends>().Where(x => x.user_id == userid).List<MessengerFriends>();

                IList<MessengerFriends> m_friends_query2 = m_session.QueryOver<MessengerFriends>().Where(x => x.friend_id == userid).List<MessengerFriends>();




                foreach (MessengerFriends f in m_friends_query)
                {
                    m_friends.Add(new MessengerFriend(f.friend_id));

                }

                foreach (MessengerFriends g in m_friends_query2)
                {
                    m_friends.Add(new MessengerFriend(g.user_id));
                }

                m_session.Close();

            }


            return m_friends;
        }

        public static UserModel returnEntityById(int id)
        {
            UserModel m_model;

            using (ISession m_session = DatabaseManager.openSession())
            {
                m_model = m_session.Query<UserModel>().Where(x => x.user_id == id).FirstOrDefault();
                m_session.Close();
            }

            return m_model;
                
        }
        public static UserModel returnEntityByName(string name)
        {
            UserModel m_model;

            using (ISession m_session = DatabaseManager.openSession())
            {
                m_model = m_session.Query<UserModel>().Where(x => x.user_name == name).FirstOrDefault();
                m_session.Close();
            }

            return m_model;

        }
        public static Boolean checkIfUserExists(String name)
        {
            Boolean result;

            using (ISession m_session = DatabaseManager.openSession())
            {
                result = m_session.QueryOver<UserModel>().Where(x => x.user_name == name).RowCount() > 0;
                m_session.Close();
            }

            return result;
        }

        public static void updatePersistentMessage(String name, String mission)
        {
            UserModel m_user = returnEntityByName(name);

            using (ISession m_session = DatabaseManager.openSession())
            {
                m_user.user_console_mission = mission;
                m_session.Update(m_user);
                m_session.Flush();
                m_session.Close();
            }
        }

        public static void createNewFriendRequest(GameSession s, int id)
        {
            using (ISession m_session = DatabaseManager.openSession())
            {
                MessengerRequests m_request = new MessengerRequests();
                m_request.to_id = id;
                m_request.from_id = s.returnUser.user_id;

                m_session.Save(m_request);
                m_session.Flush();
                m_session.Close();
            }
        }

        public static Boolean FriendRequestsExists(int toId, int fromId)
        {
            Boolean m_result;

            using (ISession m_session = DatabaseManager.openSession())
            {
                m_result = m_session.QueryOver<MessengerRequests>().Where(x => x.to_id == toId).And(x => x.from_id == fromId).RowCount() > 0;
                m_session.Close();
            }

            return m_result;
        }

        public static int returnUserIdByName(String name)
        {
            UserModel m_model;

            using (ISession m_session = DatabaseManager.openSession())
            {
                m_model = m_session.Query<UserModel>().Where(x => x.user_name == name).FirstOrDefault();
                m_session.Close();
            }

            return m_model.user_id;

        }

        public static void saveFriendBuddy(GameSession s, int id)
        {
            using (ISession m_session = openSession())
            {
                MessengerFriends m_friend = new MessengerFriends();
                m_friend.user_id = s.returnUser.user_id;
                m_friend.friend_id = id;
                m_session.Save(m_friend);
                m_session.Flush();
                m_session.Close();
            }
        }

        public static MessengerFriends return_friend_buddy(GameSession s, int id)
        {
            MessengerFriends m_friend;

            
            using (ISession m_session = openSession())
            {
                m_friend = m_session.QueryOver<MessengerFriends>().Where(x => x.user_id == s.returnUser.user_id).And(x => x.friend_id == id).SingleOrDefault();

                //need to find out how to do or statement
                if (m_friend == null)
                {
                    m_friend = m_session.QueryOver<MessengerFriends>().Where(x => x.user_id == id).And(x => x.friend_id == s.returnUser.user_id).SingleOrDefault();
                }

                m_session.Close();
            }

            return m_friend;
        }
        public static void removeFriendBuddy(GameSession s, int id)
        {
            using (ISession m_session = openSession())
            {
                MessengerFriends m_friend = return_friend_buddy(s, id);
                m_session.Delete(m_friend);
                m_session.Flush();
                m_session.Close();
            }

            s.return_messenger.return_friends.Clear();
            s.return_messenger.return_friends = returnFriendsIDs(s.returnUser.user_id);

        }

        public static MessengerRequests return_friend_request(int from_id, int to_id)
        {
            MessengerRequests m_request;

            using (ISession m_session = openSession())
            {
                m_request = m_session.QueryOver<MessengerRequests>().Where(x => x.from_id == from_id).And(x => x.to_id == to_id).SingleOrDefault();
                m_session.Close();
            }

            return m_request;
        }

        public static void removeFriendRequest(int from_id, int to_id)
        {
            MessengerRequests m_to = return_friend_request(from_id, to_id);

            using (ISession m_session = openSession())
            {
                m_session.Delete(m_to);
                m_session.Flush();
                m_session.Close();
            }
        }

        public static IList<MessengerRequests> returnAllFriendRequests(int id)
        {
            IList<MessengerRequests> m_requests;

            using (ISession m_session = openSession())
            {
                m_requests = m_session.QueryOver<MessengerRequests>().Where(x => x.to_id == id).List();

                m_session.Close();
            }

            return m_requests;
        }

        public static void removeAllFriendRequests(int to_id)
        {

            using (ISession m_session = openSession())
            {
                IList<MessengerRequests> m_requests = returnAllFriendRequests(to_id);

                foreach (MessengerRequests request in m_requests)
                {
                    m_session.Delete(request);
                }

                m_session.Flush();
                m_session.Close();
            }
        }

        public static MessengerMessages createNewMessage(int recepient_id, int sender_id, string msg, DateTime time)
        {
            MessengerMessages m_message;

            using (ISession m_session = openSession())
            {
                m_message = new MessengerMessages();

                m_message.sender_id = sender_id;
                m_message.recepient_id = recepient_id;
                m_message.message_text = msg;
                m_message.time_sent = DateTime.Now;
                m_session.Save(m_message);

                m_session.Flush();
                m_session.Close();
            }

            return m_message;
        }

        public static void markMessageAsRead(int id)
        {
            using (ISession m_session = openSession())
            {
                MessengerMessages m_message = m_session.QueryOver<MessengerMessages>().Where(x => x.message_id == id).SingleOrDefault();
                m_session.Delete(m_message);
                m_session.Flush();
                m_session.Close();
            }
        }

        public static IList<MessengerMessages> returnAllMessages(int id)
        {
            IList<MessengerMessages> m_messages;

            using (ISession m_session = openSession())
            {
                m_messages = m_session.QueryOver<MessengerMessages>().Where(x => x.recepient_id == id).List();
                m_session.Close();
            }

            return m_messages;

        }

        public static IList<NavigatorCategory> return_user_flat_cats()
        {
            IList<NavigatorCategory> m_user_flat_cats;

            using (ISession m_session = openSession())
            {
                m_user_flat_cats = m_session.QueryOver<NavigatorCategory>().Where(x => x.category_type == 2).List();
                m_session.Close();
            }

            return m_user_flat_cats;
        }

    }
}