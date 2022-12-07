using Emulator.Game.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Game.Maps
{
    public class NavigatorPublicsMap : ClassMap<NavigatorPublics>
    {
        public NavigatorPublicsMap()
        {
            Id(x => x.room_id).GeneratedBy.Identity();
            Map(x => x.room_name);
            Map(x => x.room_current_visitors);
            Map(x => x.room_max_visitors);
            Map(x => x.room_description);
            Map(x => x.room_cct);
            Map(x => x.room_category_id);
            Table("navigator_publics");

        }
    }
}
