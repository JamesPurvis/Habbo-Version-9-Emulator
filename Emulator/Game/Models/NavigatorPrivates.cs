using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Game.Models
{
    public class NavigatorPrivates
    {
        public virtual int room_id { get; set; }
        public virtual string room_name { get; set; }

        public virtual string room_model { get; set; }

        public virtual Boolean show_owner { get; set; }

        public virtual string room_owner { get; set; }

        public virtual string room_status { get; set;  }

        public virtual int room_visitors { get; set; }

        public virtual int room_max_visitors { get; set; }

        public virtual string room_description { get; set; }

        public virtual int room_category_id { get; set; }

        public virtual Boolean room_all_super { get; set; }

        public virtual string room_password { get; set; }

    }
}
