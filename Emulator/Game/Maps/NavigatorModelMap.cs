using Emulator.Game.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Game.Maps
{
    public class NavigatorModelMap : ClassMap<NavigatorModels>
    {
        public NavigatorModelMap()
        {
            Id(x => x.id).GeneratedBy.Identity();
            Map(x => x.model_name);
            Map(x => x.model_map);
            Map(x => x.model_door);
            Map(x => x.model_rows);
            Map(x => x.model_columns);
            Table("navigator_models");
        }
    }
}
