using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Game.Models
{
    public class NavigatorCategory
    {
        public virtual int category_id { get; set; }
        public virtual string category_name { get; set; }

        public virtual int category_type { get; set; }

        public virtual int category_parent_id { get; set; }

    }
}
