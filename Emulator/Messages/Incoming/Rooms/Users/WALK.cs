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
using Roy_T.AStar.Graphs;
using NHibernate.Classic;

namespace Emulator.Messages.Incoming.Rooms.Users
{
    public class WALK : MessageEvent
    {
        public void invokeEvent(HabboRequest r, GameSession s)
        {
           
               string m_goal = r.return_body();

                int goal_x = Utils.Base64Encoding.decodeString(m_goal.Substring(0, 2));
                int goal_y = Utils.Base64Encoding.decodeString(m_goal.Substring(2, 2));

        
            if (s.return_room_user.m_goal_x != goal_x || s.return_room_user.m_goal_y != goal_y)
            {
                if (s.return_room_user.m_current_x != goal_x || s.return_room_user.m_current_y != goal_y)
                {
                    Console.WriteLine("walking to " + goal_x + " " + goal_y);
                    s.return_room_instance.return_instance_interactor.startUserMovement(s.return_room_user, goal_x, goal_y, false);
              
                    
                }
            }
        }
    }
}
