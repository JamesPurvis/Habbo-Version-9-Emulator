using Emulator.Network.Session;
using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roy_T.AStar.Grids;
using Roy_T.AStar.Primitives;
using Roy_T.AStar.Paths;
using DotNetty.Buffers;

namespace Emulator.Messages.Incoming.Rooms.Users
{
    public class WALK : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
               string m_goal = r.return_body();

                int goal_x = Utils.Base64Encoding.decodeString(m_goal.Substring(0, 2));
                int goal_y = Utils.Base64Encoding.decodeString(m_goal.Substring(2, 2));

                Roy_T.AStar.Paths.Path m_path = Startup.return_environment().return_pathfinder().findPath(s.return_room_instance.return_room_info.room_model, s.return_room_user.m_current_x, s.return_room_user.m_current_y, goal_x, goal_y);

                
                 s.return_room_user.StartWalking(m_path, goal_x, goal_y, s);



   


            
        }
    }
}
