using Emulator.Game.Maps;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Game.Models
{
    public class NavigatorPublics
    {
        public virtual int room_id { get; set; }
        public virtual string room_name { get; set; }

        public virtual int room_current_visitors { get; set; }

        public virtual int room_max_visitors { get; set; }

        public virtual int room_category_id { get; set; }

        public virtual string room_description { get; set; }

        public virtual string room_cct { get; set; }
    }
}
