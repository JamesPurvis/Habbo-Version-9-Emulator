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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Emulator.Utils;

public static class SpecialMath
{
    private static Random random = new Random();

    public static int NearestSuperiorPowerOfTwo(int input)
    {
        return (int) Math.Pow(2, Math.Ceiling(
            Math.Log(input) / Math.Log(2)));
    }

    public static int RandomNumber(int min, int max)
    {
        return random.Next(min, max + 1);
    }

    public static int WorkDirection(int oldx, int oldy, int newx, int newy)
    {
        //Moved up/down  
        if (oldx == newx)
        {
            if (oldy < newy)
            {
                //South  
                return 4;
            }
            else
            {
                return 0;
            }

        } //Moved Left  
        else if (oldx > newx)
        {
            if (oldy == newy)
            {
                return 6;
            }
            else if (oldy < newy)
            {
                return 5;
            }
            else
            {
                return 7;
            }

        } //Moved Right  
        else if (oldx < newx)
        {
            if (oldy == newy)
            {
                return 2;
            }
            else if (oldy < newy)
            {
                return 3;
            }
            else
            {
                return 1;
            }
        }

        return 0;
    }

    public static long nanoTime()
    {
        long nano = 10000L * Stopwatch.GetTimestamp();
        nano /= TimeSpan.TicksPerMillisecond;
        nano *= 100L;
        return nano;
    }
}
