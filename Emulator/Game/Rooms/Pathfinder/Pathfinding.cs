using NHibernate.Util;
using Roy_T.AStar.Grids;
using Roy_T.AStar.Paths;
using Roy_T.AStar.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Game.Room
{
    public class Pathfinding
    {
        private LinkedHashMap<string, Grid> m_grid_map = null;
        private PathFinder m_pathfinder;

        public Pathfinding()
        {
            m_grid_map = new LinkedHashMap<string, Grid>();
            m_pathfinder = new PathFinder();
            m_grid_map.Add("model_a", Grid.CreateGridWithLateralAndDiagonalConnections(new GridSize(columns: 12, rows: 14), new Size(Distance.FromMeters(1), Distance.FromMeters(1)), Velocity.FromKilometersPerHour(100)));
            m_grid_map.Add("model_b", Grid.CreateGridWithLateralAndDiagonalConnections(new GridSize(columns: 12,rows: 14), new Size(Distance.FromMeters(1), Distance.FromMeters(1)), Velocity.FromKilometersPerHour(100)));
        }
        public Roy_T.AStar.Paths.Path findPath(String model, int start_x, int start_y, int goal_x, int goal_y)
        {
            Grid m_grid = m_grid_map[model];

            Roy_T.AStar.Paths.Path m_path = m_pathfinder.FindPath(new GridPosition(start_x, start_y), new GridPosition(goal_x, goal_y), m_grid);

            return m_path;
        }
    }
}
