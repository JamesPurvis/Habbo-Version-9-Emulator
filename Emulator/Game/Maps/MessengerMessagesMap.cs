using Emulator.Game.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Game.Maps
{
    public class MessengerMessagesMap : ClassMap<MessengerMessages>
    {
        public MessengerMessagesMap()
        {
            Id(x => x.message_id);
            Map(x => x.sender_id);
            Map(x => x.message_text);
            Map(x => x.recepient_id);
            Map(x => x.time_sent);
            Table("messenger_messages");
        }
    }
}
