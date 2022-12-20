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
using Roy_T.AStar.Grids;
using Roy_T.AStar.Primitives;
using Roy_T.AStar.Serialization;
using NHibernate.Mapping.ByCode;
using System.Collections;
using Emulator.Game.Rooms.Pathfinding;
using System.Security.AccessControl;
using Microsoft.Extensions.Logging.Abstractions;
using NHibernate.Engine;

namespace Emulator.Game.Rooms
{
    public class RoomUser
    {
        public UserModel m_user_model { get; set; }
        public int m_current_x { get; set; }
        public int m_current_y { get; set; }

        public int m_goal_x { get; set; }

        public int m_goal_y { get; set; }

        public List<PathFinderNode> m_path { get; set; }

        public Boolean m_requires_update { get; set; }

        public GameSession m_game_session { get; set; }

        public RoomUserStatus[] m_user_statuses { get; set; }

        public byte m_head_rotation { get; set; }

        public byte m_body_rotation { get; set; }

        public Boolean m_override_next_tile { get; set; }

        public byte m_movement_retries { get; set; }

        public float m_current_z { get; set; }
        public RoomUser(UserModel user_model)
        {
            m_user_statuses = new RoomUserStatus[5];
            m_user_model = user_model;
            this.m_path = new List<PathFinderNode>();
        }

        public Boolean requiresUpdate()
        {
            if (m_requires_update)
            {
                return true;
            }
            else
            {
                for (int a = 0; a < 5; a++)
                {
                    if (m_user_statuses[a] != null && m_user_statuses[a].isUpdated())
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void ensureUpdate(Boolean state)
        {
            m_requires_update = state;
        }

        public Boolean addStatus(String name, string data, int lifeTimeSeconds, string action, int actionSwitchSeconds, int actionLengthSeconds)
        {
            removeStatus(name);

            for (int a = 0; a < 5; a++)
            {
                if (m_user_statuses[a] == null)
                {
                    m_user_statuses[a] = new RoomUserStatus(name, data, lifeTimeSeconds, action, actionSwitchSeconds, actionLengthSeconds);
                    m_requires_update = true;
                    return true;
                }
            }

            return false;
        }

        public Boolean removeStatus(string name)
        {
            for (int a = 0; a < 5; a++)
            {
                if (m_user_statuses[a] != null && m_user_statuses[a].m_name.Equals(name))
                {
                    m_user_statuses[a] = null;
                    m_requires_update = true;
                    return true;
                }
            }

            return false;
        }

        public Boolean hasStatus(String name)
        {
            for (int a = 0; a < 5; a++)
            {
                if (m_user_statuses[a] != null && m_user_statuses[a].m_name.Equals(name))
                {
                    return true;
                }
            }

            return false;
        }

        public void removeInteractiveStatuses()
        {
            this.removeStatus("sit");
            this.removeStatus("lay");
        }

        public void clearPath()
        {
            this.m_goal_x = -1;
            this.m_goal_y = 0;
            this.m_path.Clear();
        }

        public String returnUserStatusString()
        {
            StringBuilder m_builder = new StringBuilder();

            m_builder.Append(m_game_session.returnUser.user_id);
            m_builder.Append(" ");
            m_builder.Append(m_game_session.return_room_user.m_current_x);
            m_builder.Append(",");
            m_builder.Append(m_game_session.return_room_user.m_current_y);
            m_builder.Append(",");
            m_builder.Append("0.0");
            m_builder.Append(",");
            m_builder.Append(m_game_session.return_room_user.m_head_rotation);
            m_builder.Append(",");
            m_builder.Append(m_game_session.return_room_user.m_body_rotation);
            m_builder.Append("/");

            for (int a = 0; a < 5; a++)
            {
                RoomUserStatus m_status = m_user_statuses[a];

                if (m_status != null)
                {
                    if (m_status.checkStatus())
                    {
                        m_builder.Append(m_status.m_name);

                        if (m_status.m_data != null)
                        {
                            m_builder.Append(' ');
                            m_builder.Append(m_status.m_data);
                        }

                        m_builder.Append('/');

                    }
                    else
                    {
                        m_user_statuses[a] = null;
                    }
                }
            }


            return m_builder.ToString();
        }

        public void angleHeadTo(int toX, int toY)
        {
            int diff = this.m_body_rotation - Utils.SpecialMath.WorkDirection(this.m_current_x, this.m_current_y, toX, toY);

            if (this.m_body_rotation % 2 == 0)
            {
                if (diff > 0)
                {
                    this.m_head_rotation = (byte)(this.m_body_rotation - 1);
                }

                else if (diff < 0)
                {
                    this.m_head_rotation = (byte)(this.m_body_rotation + 1);
                }

                else
                {
                    this.m_head_rotation = this.m_body_rotation;
                }
            }

            this.ensureUpdate(true);


        }
    }
    }
        