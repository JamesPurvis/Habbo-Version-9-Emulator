using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Game.Models
{
    public class NavigatorFavorites 
    {
        public virtual int id { get; set; }
        public virtual int owner_id { get; set; }

        public virtual int room_id { get; set; }

        public virtual int room_type { get; set; }
        
    }
}
