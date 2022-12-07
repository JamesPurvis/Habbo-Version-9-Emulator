using Emulator.Game.Database;
using Emulator.Network.Session;
using MySqlX.XDevAPI.Common;
using NHibernate;
using System;

namespace Emulator.Game.Models
{
    public class UserModel
    {

        public virtual int user_id { get; set; }

        public virtual string user_name { get; set; }


        public virtual string user_password { get; set; }

        public virtual string user_figure { get; set; }

        public virtual string user_email { get; set; }

        public virtual char user_gender { get; set; }
        
        public virtual string user_birthday { get; set; }

        public virtual int user_credits { get; set; }

        public virtual string user_mission { get; set; }
         
        public virtual string user_console_mission { get; set; }

        public virtual DateTime user_last_visited { get; set; }



    }
}

