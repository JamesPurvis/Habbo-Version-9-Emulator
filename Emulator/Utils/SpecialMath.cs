using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Utils
{
    public static class SpecialMath
    {
        //credit to thor for this function
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
    }
}

