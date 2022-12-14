using Emulator.Game.Database;
using Emulator.Game.Rooms;
using Emulator.Messages.Outgoing.Room;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Incoming.Rooms
{
    public class G_USERS : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
            RoomUser m_room_user = Startup.return_environment().return_room_manager().returnNewRoomUser(s.returnUser, s.return_room_instance);
           Startup.return_environment().return_room_manager().mapToRoom(m_room_user, s.return_room_instance);
            s.return_room_user = m_room_user;
            m_room_user.m_game_session = s;
            s.return_room_user.m_current_x = int.Parse(DatabaseManager.return_door(s.return_room_instance).Split(",")[0]);
            s.return_room_user.m_current_y = int.Parse(DatabaseManager.return_door(s.return_room_instance).Split(",")[1]);
            s.return_room_instance.SendToRoom(new UsersReply(s.return_room_instance, s));
        }
    }
}
