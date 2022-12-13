using Emulator.Game.Database;
using Emulator.Network.Session;
using Emulator.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Messages.Outgoing.Room
{
    public class HeightmapReply : MessageComposer
    {
        private GameSession game_session;
        public HeightmapReply(GameSession s)
        {
            this.game_session = s;
        }
        public void compose(HabboResponse response)
        {
            string m_heightmap = DatabaseManager.return_heightmap(game_session.return_room_instance);
            response.writeString(m_heightmap);
        }

        public short return_header_id()
        {
            return 31;
        }
    }
}
