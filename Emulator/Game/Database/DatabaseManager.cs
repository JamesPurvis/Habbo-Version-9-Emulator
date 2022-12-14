using Emulator.Game.Messenger;
using Emulator.Game.Models;
using Emulator.Game.Rooms;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using MySqlX.XDevAPI.Common;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Dialect;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Transform;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections;
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
               .Database(MySQLConfiguration.Standard.ConnectionString("Server=localhost;Database=habboserver;Uid=root;Pwd=$$Newnew99;"))
              .Mappings(m => m.FluentMappings
             .AddFromAssemblyOf<Environment>())
             .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true))
             .BuildSessionFactory();

            }
            catch(Exception e)
            {
                Logging.Logging.m_Logger.Error(e.Message);
            }
            
            finally
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

        public static IList<NavigatorRooms> returnRoomByOwner(String owner)
        {
            IList<NavigatorRooms> m_list = null;

            using (ISession m_session = openSession())
            {
                m_list = m_session.Query<NavigatorRooms>().Where(x => x.room_owner == owner).ToList();

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


        public static int createNewRoom(string name, string model, string status, int showowner)
        {
            NavigatorRooms m_new_room = new NavigatorRooms();

            using (ISession m_session = openSession())
            {
                m_new_room.room_name = name;
                m_new_room.room_model = model;
                m_new_room.room_status = status;
                m_new_room.show_owner = (showowner == 1);

                m_session.Save(m_new_room);
                m_session.Flush();
                m_session.Close();
            }

            return m_new_room.room_id;
        }

        public static NavigatorRooms return_user_room(int id)
        {
            NavigatorRooms m_room;

            using (ISession m_session = openSession())
            {
                m_room = m_session.QueryOver<NavigatorRooms>().Where(x => x.room_id == id).SingleOrDefault();
                m_session.Close();
            }

            return m_room;
        }
        public static void UpdateUserRoom(NavigatorRooms room)
        { 
            using (ISession m_session = openSession())
            {
                m_session.Update(room);
                m_session.Flush();
                m_session.Close();
            }
        }

        public static IList<NavigatorRooms> searchForRooms(String query)
        {
            string m_owner_name = query.Replace("%", "");
            Console.WriteLine("This is query: " + query);
            IList<NavigatorRooms> m_search_result;

            using (ISession m_session = openSession())
            {
               // if (String.IsNullOrEmpty(query.Replace("%","")))
               // {
               //     m_search_result = m_session.CreateSQLQuery("SELECT * FROM navigator_privates ORDER BY room_id ASC, LIMIT 10").SetResultTransformer((Transformers.AliasToBean<NavigatorPrivates>())).List<NavigatorPrivates>().ToList();
               // }
               // else
              //  {
                    m_search_result = m_session.CreateSQLQuery("SELECT * FROM navigator_rooms WHERE room_name LIKE :query OR room_owner = :query2").SetParameter("query", query).SetParameter("query2", m_owner_name).SetResultTransformer((Transformers.AliasToBean<NavigatorRooms>())).List<NavigatorRooms>().ToList();
                //}

                m_session.Close();
            }

            return m_search_result;
        }

        public static NavigatorFavorites returnFavoriteRoom(int room_id, int type, int user_id)
        {
            NavigatorFavorites m_instance;

            using (ISession m_session = openSession())
            {
                m_instance = m_session.QueryOver<NavigatorFavorites>().Where(x => x.room_id == room_id).And(x => x.room_type == type).And(x => x.owner_id == user_id).SingleOrDefault();
                m_session.Close();
            }

            return m_instance;
        }

        public static IList<NavigatorFavorites> returnAllFavoriteRooms(int owner_id)
        {
            IList<NavigatorFavorites> m_favorites;

            using (ISession m_session = openSession())
            {
                m_favorites = m_session.QueryOver<NavigatorFavorites>().Where(x => x.owner_id == owner_id).List();
                m_session.Close();
            }

            return m_favorites;
        }

        public static NavigatorRooms returnFavoriteRoomInstance(int room_id)
        {
            NavigatorRooms m_instance;

            using(ISession m_session = openSession())
            {
    
              m_instance = m_session.Query<NavigatorRooms>().Where(x => x.room_id == room_id).SingleOrDefault();

                m_session.Close();
            }

            return m_instance;
        }

        public static void addFavoriteRoom(int room_type, int room_id, int user_id)
        {
            NavigatorFavorites m_new_favorite = new NavigatorFavorites();

            using (ISession m_session = openSession())
            {
                m_new_favorite.room_id = room_id;
                m_new_favorite.room_type = room_type;
                m_new_favorite.owner_id = user_id;

                m_session.Save(m_new_favorite);
                m_session.Flush();
                m_session.Close();
            }
        }

        public static void deleteFavoriteRoom(NavigatorFavorites instance)
        {
            using (ISession m_session = openSession())
            {
                m_session.Delete(instance);
                m_session.Flush();
                m_session.Close();
            }
        }

        public static int return_guest_count_favorite(int user_id)
        {
            int m_count;

            using (ISession m_session = openSession())
            {
                m_count = m_session.QueryOver<NavigatorFavorites>().Where(x => x.room_type == 0).RowCount();
                m_session.Close();
            }

            return m_count;
        }

        public static void updateUserAccount(UserModel m)
        {
            using (ISession m_session = openSession())
            {
                m_session.Update(m);
                m_session.Flush();
                m_session.Close();
            }
        }

        public static string return_heightmap(Emulator.Game.Rooms.Room instance)
        {
            string m_model_name = instance.return_room_info.room_model;
            string m_heightmap;

            using (ISession m_session = openSession())
            {
                m_heightmap = m_session.QueryOver<NavigatorModels>().Where(x => x.model_name == m_model_name).SingleOrDefault().model_map;
            }

            return m_heightmap.Replace('|', Convert.ToChar(13));
        }

        public static string return_door(Emulator.Game.Rooms.Room instance)
        {
            string m_model_name = instance.return_room_info.room_model;
            string m_model_door;

            using (ISession m_session = openSession())
            {
                m_model_door = m_session.QueryOver<NavigatorModels>().Where(x => x.model_name == m_model_name).SingleOrDefault().model_door;
            }

            return m_model_door;
        }


    }
}