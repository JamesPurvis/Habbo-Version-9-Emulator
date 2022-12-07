using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Game.Maps
{
    public class UserMap : ClassMap<Models.UserModel>
    {
        public UserMap()
        {
            Id(x => x.user_id).GeneratedBy.Identity();
            Map(x => x.user_name);
            Map(x => x.user_password);
            Map(x => x.user_credits);
            Map(x => x.user_mission);
            Map(x => x.user_figure);
            Map(x => x.user_email);
            Map(x => x.user_gender);
            Map(x => x.user_birthday);
            Map(x => x.user_console_mission);
            Map(x => x.user_last_visited);
            Table("Users");
        }
    }
}