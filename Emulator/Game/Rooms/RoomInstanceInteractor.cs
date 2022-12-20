using DotNetty.Common.Utilities;
using Emulator.Game.Models;
using Emulator.Game.Rooms.Pathfinding;
using Emulator.Messages;
using Emulator.Messages.Outgoing.Room;
using Emulator.Messages.Outgoing.Room.Users;
using NHibernate.Cfg.Loquacious;
using Remotion.Linq.Parsing.Structure.IntermediateModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Game.Rooms
{
    public class RoomInstanceInteractor
    {
        public Room return_room_instance { get; set; }

        public byte[,] return_room_map { get; set; }

        public float[,] return_height_map { get; set; }

        public Boolean[,] return_user_map { get; set; }

        public char[,] return_client_map { get; set; }

        public RoomTile[,] return_redirect_map { get; set; }

        private Thread return_interactor_thread { get; set; }


        private int CLIENT_FRAMETIME = 470;

        private Boolean return_is_alive { get; set; }

        public RoomInstanceInteractor(Room instance)
        {
            this.return_interactor_thread = new Thread(new ThreadStart(run));
            this.return_room_instance = instance;
            this.return_room_map = new byte[0, 0];
            this.return_height_map = new float[0, 0];
            this.return_client_map = new char[0, 0];
            this.return_redirect_map = new RoomTile[0, 0];
            this.return_user_map = null;
            this.generateHeightMap();
            Logging.Logging.m_Logger.Info("Room Map: " + this.generateRoomMap());
            Logging.Logging.m_Logger.Info("Client map: " + this.generateClientMap());
            Logging.Logging.m_Logger.Info("User map: " + this.generateUserMap());
            Logging.Logging.m_Logger.Info("Height map: " + this.GenerateHeightMatrix());
            start();
        }

        public void start()
        {
            return_is_alive = true;
            return_interactor_thread.Start();
        }

        public void stop()
        {
            return_is_alive = false;
        }
        public void generateHeightMap()
        {
            //thanks blunk, what are 2d arrays anyway im stupid
            string m_default_map = return_room_instance.return_room_model.model_map;

            string[] axes = m_default_map.Split(" ");

            int maxX = axes[0].Length;
            Console.WriteLine(maxX + " " + "is the max y");
            int maxY = axes.Length;
            Console.WriteLine(maxY + " " + "is the max y");

            byte[,] m_temp_map = new byte[maxX, maxY];
            float[,] m_temp_height_map = new float[maxX, maxY];
            char[,] m_clientmap = new char[maxX, maxY];

            RoomTile[,] m_redirect_map = new RoomTile[maxX, maxY];

            if (return_user_map == null)
            {
                return_user_map = new bool[maxX, maxY];
            }

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0 ; x < maxX; x++)
                {
                    char tile = axes[y][x];

                    m_clientmap[x, y] = tile;

                    if (tile != 'x' && Char.IsDigit(tile))
                    {
                        m_temp_map[x, y] = 0;
                        m_temp_height_map[x, y] = float.Parse(Char.ToString(tile));
                    }
                    else
                    {
                        m_temp_map[x, y] = 1;
                    }
                }
            }

            this.return_room_map = m_temp_map;
            this.return_height_map = m_temp_height_map;
            this.return_redirect_map = m_redirect_map;
            this.return_client_map = m_clientmap;

            int m_door_x = int.Parse(return_room_instance.return_room_model.model_door.Split(",")[0]);
            int m_door_y = int.Parse(return_room_instance.return_room_model.model_door.Split(",")[1]);

            m_temp_map[m_door_x, m_door_y] = 0;

            m_temp_height_map[m_door_x, m_door_y] = 0;

            m_clientmap[m_door_x, m_door_y] = '0';



        }

        public string generateClientMap()
        {
            StringBuilder m = new StringBuilder();

            int maxX = return_room_map.GetUpperBound(0) + 1;
            Console.WriteLine(maxX);
            int maxY = return_room_map.GetUpperBound(1) + 1;
            Console.WriteLine(maxY);

            for(int y = 0; y < maxY; y++)
            {
                for(int x = 0; x < maxX; x++)
                {
                    m.AppendLine(return_client_map[x, y] + " ");
                    
                }
            }

            return m.ToString();
        }

        public string generateUserMap()
        {
            StringBuilder m = new StringBuilder();

            int maxX = return_room_map.GetUpperBound(0) + 1;
            Console.WriteLine(maxX);
            int maxY = return_room_map.GetUpperBound(1) + 1;
            Console.WriteLine(maxY);

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    m.Append(return_user_map[x, y] + " ");

                }
            }

            return m.ToString();
        }

        public string generateRoomMap()
        {
            StringBuilder m = new StringBuilder();

            int maxX = return_room_map.GetUpperBound(0) + 1;
            Console.WriteLine(maxX);
            int maxY = return_room_map.GetUpperBound(1) + 1;
            Console.WriteLine(maxY);

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    m.Append(return_room_map[x, y]);
                    

                }
            }

            return m.ToString();
        }
       
        public string GenerateHeightMatrix()
        {
            StringBuilder m = new StringBuilder();

            int maxX = return_room_map.GetUpperBound(0) + 1;
            Console.WriteLine(maxX);
            int maxY = return_room_map.GetUpperBound(1) + 1;
            Console.WriteLine(maxY);

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    m.Append(return_height_map[x,y] + " ");


                }
            }

            return m.ToString();
        }
        public String generateHeightMapString()
        {
            int maxX = return_room_map.GetUpperBound(0) + 1;
            Console.WriteLine(maxX);
            int maxY = return_room_map.GetUpperBound(1) + 1;
            Console.WriteLine(maxY);
            StringBuilder m_builder = new StringBuilder((maxX * maxY) + maxY);

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {

                    m_builder.Append(return_client_map[x, y]);
                }

                m_builder.Append("|");
            }

            return m_builder.ToString();
        }

        public void run()
        {
            while (return_is_alive)
            {
                
                long m_task_start = Utils.SpecialMath.nanoTime();

                this.refreshUsers();

                double m_sleep_time = (Utils.SpecialMath.nanoTime() - m_task_start);

                m_sleep_time /= 1000000;

                m_sleep_time = (CLIENT_FRAMETIME - m_sleep_time);

                if (m_sleep_time > 0.0)
                {
                    Thread.Sleep((int)m_sleep_time);
                }
            }
        }

        public void refreshUsers()
        {
            Boolean SendUpdates = false;
            StringBuilder msg = new StringBuilder();

            foreach (RoomUser u in return_room_instance.return_room_users)
            {
                if (u.m_goal_x >  0)
                {
                    this.refreshUsers(u, msg);
                    SendUpdates = true;
                }
            }

            if (SendUpdates == true)
            {
                return_room_instance.SendToRoom(new UserStatusReply(msg.ToString()));
            }
        }

        public void refreshUsers(RoomUser u, StringBuilder msg)
        {
            Boolean appendStatus = true;

            u.ensureUpdate(false);

            if (u.m_goal_x != -1)
            {
                if (u.m_current_x == u.m_goal_x && u.m_goal_y == u.m_current_y)
                {
                    u.clearPath();

                    
                }
                else
                {
                    
                   PathFinderNode next;

                    if (u.m_override_next_tile)
                    {
                        u.m_override_next_tile = false;
                        next = new PathFinderNode();
                        next.X = u.m_goal_x;
                        next.Y = u.m_goal_y;
                    }

                    else
                    {
                        // Pop the next tile from the path
                        next = getMovingUserNextTile(u);
                        Console.WriteLine("The next tile from path is: " + next.X);
                        Console.WriteLine("the next tile from path is: " + next.Y);
                    }
                    
                    // Next tile available?
                    if (next.Equals(default(PathFinderNode)))
                    {
                        Console.WriteLine("The path is null");
                        
                        // Add a try to the counter
                        u.m_movement_retries++;

                        // Re-find path or give up?
                        if (u.m_movement_retries < 3)
                        {
                           this.startUserMovement(u, u.m_goal_x, u.m_goal_y, false);
                           
                        }
                        else
                        {
                            // Give up on this path
                            u.m_movement_retries = 0;
                            u.clearPath();
                        }

                    }
                    else
                    {
                        // Moved a tile
                        u.m_movement_retries = 0;

                        // Block & unblock tiles
                        return_user_map[u.m_current_x, u.m_current_y] = false;
                        return_user_map[next.X, next.Y] = true;


                        // Rotate users body
                        byte rotation = (byte) Utils.SpecialMath.WorkDirection(u.m_current_x, u.m_current_y, u.m_goal_x, u.m_goal_y);
                       

                        // Apply rotation
                        u.m_head_rotation = rotation;
                        u.m_body_rotation = rotation;

                        // Remove interactive statuses
                        u.removeInteractiveStatuses();

                        // Append status
                        msg.Append(u.returnUserStatusString());
                        msg.Append("mv " + next.X + "," + next.Y + "," + (int)return_height_map[next.X, next.Y] + "/");
                        appendStatus = false;

                        // Serverside movement of user
                        u.m_current_x= next.X;
                        Console.WriteLine("The next is: " + next.X);
                        u.m_current_y= next.Y;
                        Console.WriteLine("The next is " + next.Y);
                        u.m_current_z = return_height_map[next.X, next.Y];

                        
                    }
                }
            }

            // Append status string?
            if (appendStatus)
            {
                msg.Append(u.returnUserStatusString());
            }
        }
        public Boolean mapTileWalkable(int X, int Y)
        {
            return return_room_map[X, Y] == 0 && return_user_map[X, Y];
        }

        private PathFinderNode getMovingUserNextTile(RoomUser user)
        {
            int tileAmount = user.m_path.Count();

            Console.WriteLine("Tile amount: " + tileAmount);

            if (tileAmount > 0)
            {
                // Pop next node and check if the tile is walkable
                PathFinderNode next = user.m_path[0];

                

                

                Console.WriteLine("The map value is " + " " + return_room_map[next.X, next.Y]);

                

                if (next.Equals(null)|| return_room_map[next.X, next.Y] != 0 && return_room_map[next.X, next.Y] != 2)
                {
                    Console.WriteLine("TEST 2");
                    user.m_path.Remove(next);
                    return default(PathFinderNode);  // Next tile blocked
                }

                else if (return_user_map[next.X,next.Y])
                {
                    user.m_path.RemoveAt(0);
                    return default(PathFinderNode);
                    
                }


              user.m_path.RemoveAt(0);
                

                // Can do a shortcut?
                 Boolean CROSS_DIAGONAL = true;

                PathFinderNode node;
                Console.WriteLine("The fugging little tile amount is " + tileAmount);

                if (tileAmount > 1 && CROSS_DIAGONAL)
                {
                    Console.WriteLine("I MADE IT HERE!");
                    // Peek + test next node (already picked one, so this is like path[1])
                    node = user.m_path[0];

                    if (this.mapTileWalkable(node.X, node.Y))
                    {
                        // Can't skip more than one tile!
                        if (!(Math.Abs(user.m_current_x - node.X) > 1 || Math.Abs(user.m_current_y - node.Y) > 1))
                        {
                            byte X1 = 0;
                            byte X2 = 0;
                            if (node.X > user.m_current_x)
                            {
                                unchecked
                                {
                                    Console.WriteLine("TEST");
                                 X1 = (byte)-1;
                                 X2 = 1;
                               }
                            }
                            else
                            {
                                unchecked
                                {
                                    X1 = 1;
                                    X2 = (byte)-1;
                                }
                            }

                            if (this.mapTileWalkable(node.X + X1, node.Y) && this.mapTileWalkable(user.m_current_x + X2, user.m_current_y)) // Valid shortcut
                            {
                                next = user.m_path[0];
                                user.m_path.Remove(user.m_path[0]); // Skip this node! Yay!
                            }
                        }
                    }
                }

                // The next tile!
                return next;
            }
            else
            {
                return default(PathFinderNode);
            }
        }
            
        

        public void startUserMovement(RoomUser user, int goal_x, int goal_y, Boolean override_next)
        {
            user.clearPath();
            user.ensureUpdate(true);

            if (!this.mapTileExists(goal_x, goal_y))
            {
                return;
            }

            RoomTile m_redirect = return_redirect_map[goal_x, goal_y];

            if (m_redirect != null)
            {
                goal_x = m_redirect.X;
                goal_y = m_redirect.Y;
            }

            if (override_next)
            {
                user.m_goal_x = goal_x;
                user.m_goal_y = goal_y;
                user.m_override_next_tile = true;
            }
            else
            {
                Boolean interactiveTile = false;

                if (return_room_map[goal_x, goal_y] == 2)
                {
                    if (!return_user_map[goal_x, goal_y])
                    {
                        interactiveTile = true;
                        return_room_map[goal_x, goal_y] = 0;
                    }
    
                }


                Console.WriteLine("I am here");

             //  byte tmpByte = return_room_map[goal_x, goal_y];
           // Console.WriteLine("Room map byte: " + tmpByte);
           // Console.WriteLine("The position at " + goal_x + " " + goal_y + " " + "is " + return_room_map[goal_x, goal_y]);

           //   return_room_map[goal_x, goal_y] = 1;

                List<PathFinderNode> m_path = null;
        
                try
                {

                    m_path = calculatePath(user.m_current_x, user.m_current_y, goal_x, goal_y, return_room_map);
                }
                catch(Exception e)
                {
                    Console.WriteLine("ERROR: " + e.Message);
                }

                
                

           // return_room_map[goal_x, goal_y] = tmpByte;


                if (interactiveTile)
                {
                    return_room_map[goal_x, goal_y] = 2;
                }


                if (m_path.Count > 0)
                {
                    Console.WriteLine(m_path.Count + " " + "count inside list");
                    //PathFinderNode m_node = m_path[0];
                    //m_path.RemoveAt(0);

                    user.m_path = m_path;
                    user.m_goal_x = goal_x;
                    user.m_goal_y = goal_y;
                }
            }


        }
        public Boolean mapTileExists(int x, int y)
        {
            return (x >= 0 && x < return_room_map.Length && y >= 0 && y <= return_room_map.GetLength(0) + 1);
            
        }

        public List<PathFinderNode> calculatePath(int StartX, int StartY, int EndX, int EndY, byte[,]matrix)
        {
           

            PathFinder finder = new PathFinder(return_room_map);

            finder.Heightmap = return_height_map;
            finder.MaxAscend = 4;
            finder.MaxDescend = 4;

            Point Start = new Point(StartX, StartY);
            Point End = new Point(EndX, EndY);

            List<PathFinderNode> result = finder.FindPath(Start, End);
            
            foreach(PathFinderNode node in result)
            {
                Console.WriteLine(node.X);
            }

            Console.WriteLine(result[0]);

            if (result != null)
            {
                result.Remove(result[0]);
            }

            return result;
        }
    }
}
