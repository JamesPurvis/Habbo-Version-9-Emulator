using NHibernate.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Utils
{
    public static class UpdateAccountParser
    {
        public static LinkedHashMap<int, string> return_props(String data)
        {
            LinkedHashMap<int, string> m_map = new LinkedHashMap<int, string>();

            int mPointer = 0;

            while (mPointer < data.Length)
            {
                int mID = Base64Encoding.decodeString(data.Substring(mPointer, 2));

                if (mID == 9)
                {
                    break;
                }

                mPointer += 2;
                int mLength = Base64Encoding.decodeString(data.Substring(mPointer, 2));
                mPointer += 2;
                string item = data.Substring(mPointer, mLength);
                mPointer += mLength;
                m_map.Add(mID, item);
            }

            return m_map;
        }
    }
}

