/*
Thor Server Project
Copyright 2008 Joe Hegarty


This file is part of The Thor Server Project.

The Thor Server Project is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

The Thor Server Project is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with The Thor Server Project.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Emulator.Utils;

namespace Emulator.Game.Rooms.Pathfinding;

public struct PathFinderNode
{
    public int     F;
    public int     G;
    public int     H;  // f = gone + heuristic
    public int     X;
    public int     Y;
    public int     PX; // Parent
    public int     PY;
}


public enum PathFinderNodeType
{
    Start   = 1,
    End     = 2,
    Open    = 4,
    Close   = 8,
    Current = 16,
    Path    = 32
}

public enum HeuristicFormula
{
    Manhattan           = 1,
    MaxDXDY             = 2,
    DiagonalShortCut    = 3,
    Euclidean           = 4,
    EuclideanNoSQR      = 5,
    Custom1             = 6
}

public delegate void PathFinderDebugHandler(int fromX, int fromY, int x, int y, PathFinderNodeType type, int totalCost, int cost);

public class PathFinder : IPathFinder
{
    private byte[,]                         mGrid                   = null;
    private float[,]                        mHeightmap              = null;
    private float                           mMaximumAscend          = 1.5f;
    private float                           mMaximumDescend         = 4;
    private bool mAccountHeights                                    = false;
    private PriorityQueueB<PathFinderNode>  mOpen                   = new PriorityQueueB<PathFinderNode>(new ComparePFNode());
    private List<PathFinderNode>            mClose                  = new List<PathFinderNode>();
    private bool                            mStop                   = false;
    private bool                            mStopped                = true;
    private HeuristicFormula                mFormula                = HeuristicFormula.MaxDXDY;
    private int                             mHEstimate              = 2;
    private bool                            mTieBreaker             = true;
    private int                             mSearchLimit            = 25000;
    private double                          mCompletedTime          = 0;

    public PathFinder(byte[,] grid)
    {
        if (grid == null) throw new Exception("Pathfinder: Grid cannot be null");
        mGrid = grid;
    }

    public bool AccountHeights
    {
        get { return mAccountHeights; }
        set { mAccountHeights = value; }
    }

    public float MaxAscend
    {
        get { return mMaximumAscend; }
        set { mMaximumAscend = value; }
    }

    public float MaxDescend
    {
        get { return mMaximumDescend; }
        set { mMaximumDescend = value; }
    }

    public float[,] Heightmap
    {
        get { return mHeightmap; }
        set { mHeightmap = value; }
    }

    public bool Stopped
    {
        get { return mStopped; }
    }

    public HeuristicFormula Formula
    {
        get { return mFormula; }
        set { mFormula = value; }
    }

    public int HeuristicEstimate
    {
        get { return mHEstimate; }
        set { mHEstimate = value; }
    }

    public bool TieBreaker
    {
        get { return mTieBreaker; }
        set { mTieBreaker = value; }
    }

    public int SearchLimit
    {
        get { return mSearchLimit; }
        set { mSearchLimit = value; }
    }

    public double CompletedTime
    {
        get { return mCompletedTime; }
        set { mCompletedTime = value; }
    }

    public void FindPathStop()
    {
        mStop = true;
    }

    public List<PathFinderNode> FindPath(Point start, Point end)
    {
        lock (this)
        {
            PathFinderNode parentNode;
            bool found = false;
            int gridX = mGrid.GetUpperBound(0) + 1;
            int gridY = mGrid.GetUpperBound(1) + 1;
        
            Console.WriteLine("THE GRID X IS " + gridX);
            Console.WriteLine("THE GRID Y IS " + gridY);

            mStop = false;
            mStopped = false;
            mOpen.Clear();
            mClose.Clear();


            sbyte[,] direction;
            direction = new sbyte[4, 2] { { 0, -1 }, { 1, 0 }, { 0, 1 }, { -1, 0 } };

            parentNode.G = 0;
            parentNode.H = mHEstimate;
            parentNode.F = parentNode.G + parentNode.H;
            parentNode.X = start.X;
            parentNode.Y = start.Y;
            parentNode.PX = parentNode.X;
            parentNode.PY = parentNode.Y;
            mOpen.Push(parentNode);

            Console.WriteLine("The parent node x is " + parentNode.X + " " + parentNode.Y);

            while (mOpen.Count > 0 && !mStop)
            {
                
                parentNode = mOpen.Pop();

                Console.WriteLine("pop it bitch");

                if (parentNode.X == end.X && parentNode.Y == end.Y)
                {
                    Console.WriteLine("We at our goal.");
                    mClose.Add(parentNode);
                    found = true;
                    break;
                }

                if (mClose.Count > mSearchLimit)
                {
                    Console.WriteLine("Too many tiles");
                    mStopped = true;
                    return null;
                }

                //Lets calculate each successors
                for (int i = 0; i < 4; i++)
                {
                    
                    PathFinderNode newNode;
                    newNode.X = parentNode.X + direction[i, 0];
                    newNode.Y = parentNode.Y + direction[i, 1];
                    Console.WriteLine("the new node x is " + newNode.X);
                    Console.WriteLine("the new node y is " + newNode.Y);

                    if (newNode.X < 0 || newNode.Y < 0 || newNode.X >= gridX || newNode.Y >= gridY)
                        continue;

                   
                    int newG = parentNode.G + mGrid[newNode.X, newNode.Y] + 1;
                    Console.WriteLine(newG + " " + "is  G");

                    Console.WriteLine(newG + " " + parentNode.G);
                    if (newG == parentNode.G)
                    {
                        //Unbrekeable
                        continue;
                    }

                    Console.WriteLine("ok bet height time");

                    //Take into account heights
                    if (mAccountHeights)
                    {
                        if (mMaximumDescend > -1)
                        {
                            if (mHeightmap[parentNode.X, parentNode.Y] - mMaximumDescend > mHeightmap[newNode.X, newNode.Y])
                            {
                                continue;
                            }
                        }

                        if (mMaximumAscend > -1)
                        {
                            if (mHeightmap[parentNode.X, parentNode.Y] + mMaximumAscend < mHeightmap[newNode.X, newNode.Y])
                            {
                                continue;
                            }
                        }
                    }


                   int foundInOpenIndex = -1;
                    for (int j = 0; j < mOpen.Count; j++)
                    {
                        if (mOpen[j].X == newNode.X && mOpen[j].Y == newNode.Y)
                        {
                            foundInOpenIndex = j;
                            Console.WriteLine(" " + "openIndex: " + j);
                            break;
                        }
                    }
                    if (foundInOpenIndex != -1 && mOpen[foundInOpenIndex].G <= newG)
                        continue;

                    int foundInCloseIndex = -1;
                    for (int j = 0; j < mClose.Count; j++)
                    {
                        if (mClose[j].X == newNode.X && mClose[j].Y == newNode.Y)
                        {
                            foundInCloseIndex = j;
                            Console.WriteLine("openIndex: " + j);
                            break;
                        }
                    }

                    if (foundInCloseIndex != -1 && mClose[foundInCloseIndex].G <= newG)
                        continue;

                    newNode.PX = parentNode.X;
                    newNode.PY = parentNode.Y;
                    newNode.G = newG;

                    Console.WriteLine("Parent node x:" + newNode.PX);
                    Console.WriteLine("Parent node y: " + newNode.PY);
                    switch (mFormula)
                    {
                        default:
                        case HeuristicFormula.Manhattan:
                            newNode.H = mHEstimate * (Math.Abs(newNode.X - end.X) + Math.Abs(newNode.Y - end.Y));
                            break;
                        case HeuristicFormula.MaxDXDY:
                            newNode.H = mHEstimate * (Math.Max(Math.Abs(newNode.X - end.X), Math.Abs(newNode.Y - end.Y)));
                            break;
                        case HeuristicFormula.DiagonalShortCut:
                            int h_diagonal = Math.Min(Math.Abs(newNode.X - end.X), Math.Abs(newNode.Y - end.Y));
                            int h_straight = (Math.Abs(newNode.X - end.X) + Math.Abs(newNode.Y - end.Y));
                            newNode.H = (mHEstimate * 2) * h_diagonal + mHEstimate * (h_straight - 2 * h_diagonal);
                            break;
                        case HeuristicFormula.Euclidean:
                            newNode.H = (int)(mHEstimate * Math.Sqrt(Math.Pow((newNode.X - end.X), 2) + Math.Pow((newNode.Y - end.Y), 2)));
                            break;
                        case HeuristicFormula.EuclideanNoSQR:
                            newNode.H = (int)(mHEstimate * (Math.Pow((newNode.X - end.X), 2) + Math.Pow((newNode.Y - end.Y), 2)));
                            break;
                        case HeuristicFormula.Custom1:
                            Point dxy = new Point(Math.Abs(end.X - newNode.X), Math.Abs(end.Y - newNode.Y));
                            int Orthogonal = Math.Abs(dxy.X - dxy.Y);
                            int Diagonal = Math.Abs(((dxy.X + dxy.Y) - Orthogonal) / 2);
                            newNode.H = mHEstimate * (Diagonal + Orthogonal + dxy.X + dxy.Y);
                            break;
                    }
                    if (mTieBreaker)
                    {
                        int dx1 = parentNode.X - end.X;
                        int dy1 = parentNode.Y - end.Y;
                        int dx2 = start.X - end.X;
                        int dy2 = start.Y - end.Y;
                        int cross = Math.Abs(dx1 * dy2 - dx2 * dy1);
                        newNode.H = (int)(newNode.H + cross * 0.001);
                    }
                    newNode.F = newNode.G + newNode.H;

                    mOpen.Push(newNode);
                }

                mClose.Add(parentNode);

            }

            if (found)
            {
                PathFinderNode fNode = mClose[mClose.Count - 1];
                for (int i = mClose.Count - 1; i >= 0; i--)
                {
                    if (fNode.PX == mClose[i].X && fNode.PY == mClose[i].Y || i == mClose.Count - 1)
                    {
                        fNode = mClose[i];
                    }
                    else
                        mClose.RemoveAt(i);
                }
                mStopped = true;
                return mClose;
            }
            mStopped = true;
            return null;
        }
    }

  
    internal class ComparePFNode : IComparer<PathFinderNode>
    {

        public int Compare(PathFinderNode x, PathFinderNode y)
        {
            if (x.F > y.F)
                return 1;
            else if (x.F < y.F)
                return -1;
            return 0;
        }

    }
}
 