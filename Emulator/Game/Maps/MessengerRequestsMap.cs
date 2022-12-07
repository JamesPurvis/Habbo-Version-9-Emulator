using Emulator.Game.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Game.Maps
{
    public class MessengerRequestsMap : ClassMap<MessengerRequests>
    {
        public MessengerRequestsMap()
        {
            Id(x => x.record_id).GeneratedBy.Identity();
            Map(x => x.from_id);
            Map(x => x.to_id);
            Table("messenger_requests");
        }
    }
}
