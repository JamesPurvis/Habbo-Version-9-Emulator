using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Game.Models
{
    public class MessengerFriends
    { 
        public virtual int record_id { get; set; }
        public virtual int user_id { get; set; }
        public virtual int friend_id { get; set; }
    }
}
