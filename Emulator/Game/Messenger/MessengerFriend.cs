using Emulator.Game.Database;
using Emulator.Game.Models;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Game.Messenger
{
    public class MessengerFriend
    {

        private int user_id;
        private string user_name;
        private string console_status;
        private string user_last_visited;
        private string user_figure;
        private UserModel user_model;
        private char user_gender;

        public int return_user_id
        {
            get { return user_id; }
        }

        public UserModel return_model
        {
            get { return user_model; }
        }

        public string return_user_name
        {
            get { return user_name; }
        }

        public string return_user_figure
        {
            get { return user_figure;  }
        }
      
        public string return_last_visited
        {
            get { return user_last_visited; }
        }

        public string return_console_status
        {
            get { return console_status; }
        }
        public int return_user_gender
        {
            get
            {
                if (user_gender == 'M')
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public MessengerFriend(int id)
        {
            user_model = DatabaseManager.returnEntityById(id);
            user_id = id;
            user_name = user_model.user_name;
            console_status = user_model.user_console_mission;
            user_last_visited = user_model.user_last_visited.ToString();
            user_figure = user_model.user_figure;
            user_gender = user_model.user_gender;
        }

        public void parse_activity(HabboResponse r)
        {
            if (Startup.return_environment().return_session_manager().checkIfConnected(user_name))
            {
                r.writeString("I" + "On Hotel View");

            }
            else
                r.writeString("H" + return_last_visited);
            }
            

        }


    }
