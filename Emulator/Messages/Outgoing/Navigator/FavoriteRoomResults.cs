using Emulator.Game.Database;
using Emulator.Game.Models;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using Emulator.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Outgoing.Navigator
{
    public class FavoriteRoomResults : MessageComposer
    {
        private IList<NavigatorFavorites> m_favorites;
        private GameSession m_session;

        public FavoriteRoomResults(GameSession s, IList<NavigatorFavorites> m_favorites)
        {
            this.m_favorites = m_favorites;
            m_session = s;
        }
        public void compose(HabboResponse response)
        {
            StringBuilder m_public_builder = new StringBuilder();
            StringBuilder m_private_builder = new StringBuilder();

            response.writeInt(0);
            response.writeInt(0);
            response.writeInt(2);
            response.write((char)2);
            response.writeInt(0);
            response.writeInt(0);
            response.writeInt(0);

            m_private_builder.Append(Encoding.GetEncoding("ISO-8859-1").GetString(VL64Encoding.encode(DatabaseManager.return_guest_count_favorite(m_session.returnUser.user_id))));


            foreach (NavigatorFavorites favorite in m_favorites)
            {
                if (favorite.room_type == 1)
                {
                    compilePublicList(favorite, m_public_builder);
                }
                else
                {
                    compilePrivateList(favorite, m_private_builder);
                }


            }

            response.write(m_private_builder.ToString());
            response.write(m_public_builder.ToString());
            
        }

        public void compilePrivateList(NavigatorFavorites f, StringBuilder s)
        {
            NavigatorRooms m_instance = DatabaseManager.returnFavoriteRoomInstance(f.room_id);

            s.Append(Encoding.GetEncoding("ISO-8859-1").GetString(VL64Encoding.encode((m_instance.room_id))));
            s.Append(m_instance.room_name);
            s.Append((char)2);
            s.Append(m_instance.room_owner);
            s.Append((char)2);
            s.Append(m_instance.room_status);
            s.Append((char)2);
            s.Append(Encoding.GetEncoding("ISO-8859-1").GetString(VL64Encoding.encode(m_instance.room_visitors)));
            s.Append(Encoding.GetEncoding("ISO-8859-1").GetString(VL64Encoding.encode(m_instance.room_max_visitors)));
            s.Append(m_instance.room_description);
            s.Append((char)2);



        }

        public void compilePublicList(NavigatorFavorites f, StringBuilder s)
        {
            NavigatorRooms m_instance = DatabaseManager.returnFavoriteRoomInstance(f.room_id);

            s.Append(Encoding.GetEncoding("ISO-8859-1").GetString(VL64Encoding.encode((f.room_id))));
            s.Append(Encoding.GetEncoding("ISO-8859-1").GetString(VL64Encoding.encode(1)));
            s.Append(m_instance.room_name);
            s.Append((char)2);
            s.Append(Encoding.GetEncoding("ISO-8859-1").GetString(VL64Encoding.encode((m_instance.room_visitors))));
            s.Append(Encoding.GetEncoding("ISO-8859-1").GetString(VL64Encoding.encode(m_instance.room_max_visitors)));
            s.Append(Encoding.GetEncoding("ISO-8859-1").GetString(VL64Encoding.encode(m_instance.room_category_id)));
            s.Append(m_instance.room_description);
            s.Append((char)2);
            s.Append(Encoding.GetEncoding("ISO-8859-1").GetString(VL64Encoding.encode(m_instance.room_id)));
            s.Append(Encoding.GetEncoding("ISO-8859-1").GetString(VL64Encoding.encode((0))));
            s.Append(m_instance.room_cct);
            s.Append((char)2);
            s.Append(Encoding.GetEncoding("ISO-8859-1").GetString(VL64Encoding.encode(0)));
            s.Append(Encoding.GetEncoding("ISO-8859-1").GetString(VL64Encoding.encode(1)));


        }

        public short return_header_id()
        {
            return 61;
        }


    }
}
