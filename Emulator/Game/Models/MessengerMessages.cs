using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Game.Models
{
    public class MessengerMessages
    {
        public virtual int message_id { get; set; }

        public virtual int recepient_id { get; set; }
        public virtual int sender_id { get; set; }

        public virtual string message_text { get; set; }

        public virtual DateTime time_sent { get; set; }
    }
}
