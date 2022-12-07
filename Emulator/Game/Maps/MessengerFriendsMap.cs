using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Game.Maps
{
    public class MessengerFriendsMap : ClassMap<Models.MessengerFriends>
    {
        public MessengerFriendsMap()
        {
            Id(x => x.record_id).GeneratedBy.Identity();
            Map(x => x.user_id);
            Map(x => x.friend_id);
            Table("messenger_friends");
        }
    }
}
