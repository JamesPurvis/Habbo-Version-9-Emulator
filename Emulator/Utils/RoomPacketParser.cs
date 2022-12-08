using DotNetty.Common.Utilities;
using NHibernate.Mapping;
using NHibernate.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Utils
{
    public static class RoomPacketParser
    {
        public static LinkedHashMap<int, string> returnRoomProperties(String create_packet)
        {
            LinkedHashMap<int, string> m_props = new LinkedHashMap<int, string>();

            create_packet = create_packet.Remove(0, 2);

            for(int a = 1; a <= 5; a++)
            {
                m_props.Add(a, create_packet.Split('/')[a]);
            }

            return m_props;

            
        }
    }
}
