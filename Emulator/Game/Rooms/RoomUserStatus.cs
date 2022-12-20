using NHibernate.Id.Insert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator.Game.Rooms
{
    public class RoomUserStatus
    {
        public string m_name { get; set; }

        public string m_data { get; set; }

        private string  m_action { get; set; }

        private int m_actionSwitchSeconds { get; set; }

        private int m_actionLengthSeconds { get; set; }

        private int m_actionEndTime { get; set; }

        private int m_statusEndTime { get; set; }

        private Boolean m_actionActive;
        private Boolean m_lastCheckResult;

        public RoomUserStatus(string name, string data, int lifeTimeSeconds, string action, int actionSwitchSeconds, int actionLengthSeconds)
        {
            int nowSeconds = DateTime.Now.Millisecond;
            m_name = name;
            m_data = data;

            if (lifeTimeSeconds > 0)
            {
                m_statusEndTime = nowSeconds + lifeTimeSeconds;
            }

            if (action != null)
            {
                m_action = action;
                m_actionSwitchSeconds = actionSwitchSeconds;
                m_actionLengthSeconds = actionLengthSeconds;
                m_actionEndTime = nowSeconds + actionSwitchSeconds;
            }

            m_lastCheckResult = true;
        }

        public Boolean isUpdated()
        {
            Boolean has_updated = false;

            Boolean validCheckResult = this.checkStatus();

            if (validCheckResult != m_lastCheckResult)
            {
                has_updated = true;
            }

            m_lastCheckResult = validCheckResult;

            return has_updated;
        }

        public Boolean checkStatus()
        {
            int nowSeconds = DateTime.Now.Millisecond;

            if (m_statusEndTime == 0)
            {
                return true;
            }
            else
            {
                if (m_statusEndTime < nowSeconds)
                {
                    return false;
                }
            }

            if (m_action != null)
            {
                if (m_actionEndTime < nowSeconds)
                {


                    String swap = this.m_name;
                    this.m_name = m_action;
                    m_action = swap;

                    int switchSeconds = 0;

                    if (m_actionActive)
                    {
                        switchSeconds = m_actionSwitchSeconds;
                    }
                    else
                    {
                        switchSeconds = m_actionLengthSeconds;
                    }

                    m_actionActive = !m_actionActive;
                    m_actionEndTime = nowSeconds + switchSeconds;
                    m_lastCheckResult = !m_lastCheckResult;
                }
            }

            return true;
           
        }

       

    }
}
