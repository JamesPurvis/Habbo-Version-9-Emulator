using Antlr.Runtime.Tree;
using Emulator.Game.Database;
using Emulator.Game.Models;
using NHibernate.Linq.ReWriters;
using NHibernate.Mapping.ByCode;
using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Cms;
using Roy_T.AStar.Grids;
using Roy_T.AStar.Primitives;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Game.Rooms
{
    public class Room
    {
        public int return_room_id { get; set; }
        public List<RoomUser> return_room_users { get; set; }

        public NavigatorRooms return_room_info { get; set; }

        public NavigatorModels return_room_model { get; set; }

        public RoomInstanceInteractor return_instance_interactor { get; set; }

        public Room(int id, NavigatorRooms instance)
        {
            this.return_room_id = id;
            this.return_room_users = new List<RoomUser>();
            this.return_room_info = instance;
            this.return_room_model = DatabaseManager.return_room_model(return_room_info.room_model);
            this.return_instance_interactor = new RoomInstanceInteractor(this);
        }

        public void SendToRoom(object o)
        {
            foreach (RoomUser u in return_room_users)
            {
                u.m_game_session.SendToSession(o);
            }
        }


    }
}
