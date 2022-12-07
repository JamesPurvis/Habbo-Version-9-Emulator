using Emulator.Game.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Game.Maps
{
    public class NavigatorPrivatesMap : ClassMap<NavigatorPrivates>
    {
        public NavigatorPrivatesMap()
        {
            Id(x => x.room_id).GeneratedBy.Identity();
            Map(x => x.room_name);
            Map(x => x.room_owner);
            Map(x => x.show_owner);
            Map(x => x.room_visitors);
            Map(x => x.room_max_visitors);
            Map(x => x.room_status);
            Map(x => x.room_description);
            Map(x => x.room_category_id);
            Table("navigator_privates");
        }
    }
    
}
