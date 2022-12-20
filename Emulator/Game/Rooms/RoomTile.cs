using NHibernate.Linq.Clauses;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Game.Rooms
{
    public class RoomTile
    {
        public int X;
        public int Y;

        public RoomTile(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public RoomTile(String position)
        {
            string[] coord = position.Split(',');
            this.X = position[0];
            this.Y = position[1];
        }

        public String toString()
        {
            return this.X + "," + this.Y;
        }

        public static RoomTile parse(String position)
        {
            return new RoomTile(position);
        }
    }
}
