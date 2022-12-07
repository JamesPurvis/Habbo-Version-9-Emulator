using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Emulator.Utils
{
    public class Base64Encoding
    {
        public byte NEGATIVE = 64;
        public byte POSITIVE = 65;

        public static byte[] Encode(int i, int numBytes)
        {
            byte[] bzRes = new byte[numBytes];
            for (int j = 1; j <= numBytes; j++)
            {
                int k = ((numBytes - j) * 6);
                bzRes[j - 1] = (byte)(0x40 + ((i >> k) & 0x3f));
            }

            return bzRes;
        }

        public static int decodeString(String data)
        {
            byte[] m_data = Encoding.GetEncoding("ISO-8859-1").GetBytes(data);

            return decode(m_data);
        }
        public static int decode(byte[] bzData)
        {
            int i = 0;
            int j = 0;

            for(int k = bzData.Length - 1; k >=0; k--)
            {
                int x = bzData[k] - 0x40;
                if (j > 0)
                {
                    x *= (int)Math.Pow(64.0, (double)j);
                }

                i += x;
                j++;
            }

            return i;
        }
    }
}
