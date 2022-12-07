using Emulator.Game.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Game.Maps
{
    public class NavigatorCategoryMap : ClassMap<Models.NavigatorCategory>
    {
        public NavigatorCategoryMap()
        {
            Id(x => x.category_id).GeneratedBy.Identity();
            Map(x => x.category_name);
            Map(x => x.category_parent_id);
            Map(x => x.category_type);
            Table("navigator_categories");

        }


    }
}
