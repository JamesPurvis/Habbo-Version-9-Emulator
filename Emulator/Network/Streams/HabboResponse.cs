using DotNetty.Buffers;
using DotNetty.Common.Utilities;
using Emulator.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Network.Streams
{
    //Credit to Quackster's Kepler server as an example of how to work with Raw Bytes.
    public class HabboResponse
    {

        private short m_header_id;
        private IByteBuffer m_byte_buffer;
        private Boolean m_special = false;

        public Boolean return_special
        {
            get { return m_special;  }
            set { m_special = value; }
        }
        public HabboResponse(short id, IByteBuffer buff)
        {
            this.m_header_id = id;
            this.m_byte_buffer = buff;
            Console.WriteLine(Encoding.GetEncoding("ISO-8859-1").GetString(Utils.Base64Encoding.Encode(m_header_id, 2)));
            this.m_byte_buffer.WriteBytes(Utils.Base64Encoding.Encode(m_header_id, 2));
        }

        public void write(Object obj)
        {
            if (obj != null)
            {
                this.m_byte_buffer.WriteBytes(Encoding.GetEncoding("ISO-8859-1").GetBytes(obj.ToString()));
            }
        }

        public void writeString(Object obj)
        {
            if (obj != null)
            {
                this.m_byte_buffer.WriteBytes(Encoding.GetEncoding("ISO-8859-1").GetBytes(obj.ToString()));
            }

            if (m_special == false)
            {
                this.m_byte_buffer.WriteByte(2);
            }
        }

        public void writeInt(int number)
        {
            this.m_byte_buffer.WriteBytes(VL64Encoding.encode(number));
        }

        public void writeBoolean(Boolean obj)
        {
            this.writeInt(obj ? 1 : 0);
        }

        public String returnString()
        {
            return Encoding.GetEncoding("ISO-8859-1").GetString(m_byte_buffer.Array);
        }

    }
}
