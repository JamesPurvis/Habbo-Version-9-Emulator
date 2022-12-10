using Emulator.Game.Models;
using FluentNHibernate.Mapping;
using Org.BouncyCastle.Crypto.Modes.Gcm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Game.Maps
{
    public class NavigatorFavoritesMap : ClassMap<NavigatorFavorites>
    {
        public NavigatorFavoritesMap()
        {
            Id(x => x.id).GeneratedBy.Identity();
            Map(x => x.owner_id);
            Map(x => x.room_id);
            Map(x => x.room_type);
            Table("navigator_favorites");
        }
    }
}
