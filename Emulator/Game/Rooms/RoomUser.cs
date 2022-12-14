using Emulator.Game.Models;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roy_T.AStar;
using Roy_T.AStar.Paths;
using Emulator.Messages.Incoming.Navigator;
using Roy_T.AStar.Graphs;
using Emulator.Network.Session;
using Emulator.Messages.Outgoing.Room.Users;
using Emulator.Messages.Incoming.Register;

namespace Emulator.Game.Rooms
{
    public class RoomUser
    {
        public UserModel m_user_model { get; set; }
        public int m_current_x { get; set; }
        public int m_current_y { get; set; }

        public GameSession m_game_session { get; set; }
        public RoomUser(UserModel user_model)
        {
            m_user_model = user_model;
        }

        public void HandleMovement(int goal_x, int goal_y, GameSession s, Roy_T.AStar.Paths.Path p)
        {
            string head_rotation = "";
            string body_rotation = "";

            foreach(var edge in p.Edges)
            {
                s.return_room_user.m_current_x = (int)edge.Start.Position.X;
                s.return_room_user.m_current_y = (int)edge.Start.Position.Y;
              
                    StringBuilder m_post_stat = new StringBuilder();

                    m_post_stat.Append("/mv ");
                    m_post_stat.Append(edge.End.Position.X);
                    m_post_stat.Append(",");
                    m_post_stat.Append(edge.End.Position.Y);
                    m_post_stat.Append(",");
                    m_post_stat.Append("0.0");

                head_rotation = Utils.SpecialMath.WorkDirection(m_current_x, m_current_y, (int)edge.End.Position.X, (int)edge.End.Position.Y).ToString();
                body_rotation = Utils.SpecialMath.WorkDirection(m_current_x, m_current_y, (int)edge.End.Position.X, (int)edge.End.Position.Y).ToString();


                    s.return_room_instance.SendToRoom(new UserStatusReply("mv", s, goal_x, goal_y, head_rotation, body_rotation, m_post_stat.ToString()));


                Thread.Sleep(374);

            }

            m_current_x = goal_x;
            m_current_y = goal_y;

            s.return_room_instance.SendToRoom(new UserStatusReply("", s, m_current_x, m_current_y, head_rotation, body_rotation, ""));
        }

        public Thread StartWalking(Roy_T.AStar.Paths.Path p, int goal_x, int goal_y, GameSession s)
        {
            Thread t = new Thread(() => HandleMovement(goal_x, goal_y, s, p));
            t.Start();
            return t;
        }
    }
}
