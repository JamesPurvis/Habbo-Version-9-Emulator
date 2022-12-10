using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using Emulator.Network.Session;
using Emulator.Utils;
using NHibernate.Linq.Clauses;

namespace Emulator.Network.Streams
{

    //Credit to Quackster's Kelper Emulator for the working with raw bytes.

    public class HabboRequest
    {
        private byte[] m_header;
        private IByteBuffer m_packet_buffer;
        private int m_length;

        public byte[] return_header_byte()
        {
            return m_header;
        }

        public int return_header_id()
        {
            return Base64Encoding.decode(m_header);
        }

        public string return_header_string()
        {
            return Encoding.GetEncoding("ISO-8859-1").GetString(m_header);
        }

        public int return_length()
        {
            return m_length;
        }

        public HabboRequest(IByteBuffer buff, int m_length)
        {
            this.m_packet_buffer = buff;
            this.m_header = new byte[] { buff.ReadByte(), buff.ReadByte() };
            this.m_length = m_length;

            Logging.Logging.m_Logger.Info("PACKET with length: " + m_length + " " + "[" + " " + "STRING: " + " " + return_header_string() + " " + "INT: " + " " + return_header_id() + "] ");
            Logging.Logging.m_Logger.Info(toString());

        }

        public int readBase64()
        {
            byte[] m_base_64 = new byte[] { this.m_packet_buffer.ReadByte(), this.m_packet_buffer.ReadByte() };
            Console.WriteLine(Encoding.GetEncoding("ISO-8859-1").GetString(m_base_64));
            return Base64Encoding.decode(m_base_64);
        }

        public String popString()
        {
            int m_length = readBase64();
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

        public string return_body()
        {
            byte[] remainingBytes = this.remainingBytes();

            if (remainingBytes != null)
            {
                return Encoding.GetEncoding("ISO-8859-1").GetString(remainingBytes);
            }

            return null;
        }
    }
}
