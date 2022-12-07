using NHibernate.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Utils
{
    public static class UserRegistrationParser
    {
        //Sulake REALLY out here making us parse long strings like this.
        //I hate this Request packet
        //I hate this code.
        //I hate it all.

        public static LinkedHashMap<int, string> return_parsed_data(String regData)
        {
            LinkedHashMap<int, string> m_map = new LinkedHashMap<int, string>();
            

            regData = regData.Remove(0, 2);

            int mPointer = 0;
            
            while(mPointer < regData.Length - 1)
            {
                int m_prop_case = Utils.Base64Encoding.decodeString(regData.Substring(mPointer, 2));

                if (m_prop_case == 10)
                {
                    mPointer += 9;
                    int m_prop_final_case = Utils.Base64Encoding.decodeString(regData.Substring(mPointer, 2));
                    mPointer += 2;
                    int m_prop_final_length = Utils.Base64Encoding.decodeString(regData.Substring(mPointer, 2));
                    mPointer += 2;
                    string m_prop_final = regData.Substring(mPointer, m_prop_final_length);
                    m_map.Add(m_prop_final_case, m_prop_final);
                    return m_map;
                }


                mPointer += 2;
                int m_prop_length = Utils.Base64Encoding.decodeString(regData.Substring(mPointer, 2));
                mPointer += 2;
                String m_prop = regData.Substring(mPointer, m_prop_length);
                mPointer += m_prop_length;

                m_map.Add(m_prop_case, m_prop);
            }

            return m_map;
        }
    }
}
