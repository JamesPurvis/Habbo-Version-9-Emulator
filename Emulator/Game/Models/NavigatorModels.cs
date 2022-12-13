using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Game.Models
{
    public class NavigatorModels
    {
        public virtual int id { get; set; }
        public virtual string model_name { get; set; }

        public virtual string model_map { get; set; }

        public virtual string model_door { get; set; }
    }
}
