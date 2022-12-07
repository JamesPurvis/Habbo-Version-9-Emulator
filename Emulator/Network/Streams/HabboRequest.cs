using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using Emulator.Network.Session;
using Emulator.Utils;

namespace Emulator.Network.Streams
{

    //Credit to Quackster's Kelper Emulator for the working with raw bytes.

    public class HabboRequest
    {
        private int m_header_id;
        private string m_header;
        private IByteBuffer m_packet_buffer;

        public String return_header()
        {
            return m_header;
        }

        public int return_header_id()
        {
            return m_header_id;
        }

        public HabboRequest(IByteBuffer buff)
        {
            this.m_packet_buffer = buff;
            byte[] m_header_array = new byte[] { buff.ReadByte(), buff.ReadByte() };
            m_header =  Encoding.GetEncoding("ISO-8859-1").GetString(m_header_array);
            m_header_id = Utils.Base64Encoding.decode(m_header_array);

            Logging.Logging.m_Logger.Debug(m_header_id);
            Logging.Logging.m_Logger.Debug("Received: " + toString() );

        }

        public int readBase64()
        {
            return Utils.Base64Encoding.decode(new byte[] { this.m_packet_buffer.ReadByte(), this.m_packet_buffer.ReadByte() });
        }

        public String popString()
        {
            int m_length = this.readBase64();
            byte[] m_data = this.readBytes(m_length);

            return Encoding.GetEncoding("ISO-8859-1").GetString(m_data);
        }
        public string toString()
        {
            return Encoding.GetEncoding("ISO-8859-1").GetString(m_packet_buffer.Array);
        }

        public int popInteger()
        {
            byte[] remaining = this.remainingBytes();

            int length = remaining[0] >> 3 & 7;
            remaining = readBytes(length);
            int value = VL64Encoding.decode(remaining);
               

            return value;
        }
        public byte[] remainingBytes()
        {
            this.m_packet_buffer.MarkReaderIndex();

            byte[] bytes = new byte[this.m_packet_buffer.ReadableBytes];
            this.m_packet_buffer.ReadBytes(bytes);

            this.m_packet_buffer.ResetReaderIndex();
            return bytes;
        }


        public byte[] readBytes(int length)
        {
            byte[] m_payload = new byte[length];
            this.m_packet_buffer.ReadBytes(m_payload);
            return m_payload;
        }
    }
}
