using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Game.Models
{
    public class MessengerRequests 
    {
        public virtual int record_id { get; set; }
        public virtual int from_id { get; set; }

        public virtual int to_id { get; set; }
    }
}
