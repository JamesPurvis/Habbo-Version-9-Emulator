using NHibernate.Linq.ReWriters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Utils
{
    public class VL64Encoding
    {
        public static byte NEGATIVE = 72;
        public static byte POSITIVE = 73;
        public static int MAX_INTEGER_BYTE_AMOUNT = 6;

        public static byte[] encode(int i)
        {
            byte[] wf = new byte[VL64Encoding.MAX_INTEGER_BYTE_AMOUNT];

            int pos = 0;
            int numBytes = 1;
            int startPos = pos;
            int negativeMask = i >= 0 ? 0 : 4;

            i = Math.Abs(i);

            wf[pos++] = (byte)(64 + (i & 3));

            for (i >>= 2; i != 0; i >>= VL64Encoding.MAX_INTEGER_BYTE_AMOUNT)
            {
                numBytes++;
                wf[pos++] = (byte)(64 + (i & 0x3f));
            }
            wf[startPos] = (byte)(wf[startPos] | numBytes << 3 | negativeMask);

            byte[] bzData = new byte[numBytes];

            Array.Copy(wf, 0, bzData, 0, numBytes);
            return bzData;
        }

        public static int decode(byte[] bzData)
        {
            int totalBytes = Buffer.ByteLength(bzData);
            int pos = 0;
            int v = 0;
            bool negative = (bzData[pos] & 4) == 4;
            totalBytes = bzData[pos] >> 3 & 7;
            v = bzData[pos] & 3;
            pos++;
            int shiftAmount = 2;
            for (int b = 1; b < totalBytes; b++)
            {
                v |= (bzData[pos] & 0x3f) << shiftAmount;
                shiftAmount = 2 + 6 * b;
                pos++;
            }

            if (negative == true)
                v *= -1;

            return v;
        }

        public static int decodeString(string str)
        {
            byte[] m_array = Encoding.GetEncoding("ISO-8859-1").GetBytes(str);

            return decode(m_array);
        }

       public static string encodeString(int value)
        {
            byte[] m_array = encode(value);

            return Encoding.GetEncoding("ISO-8859-1").GetString(m_array);
        }
    }
}
