using SnmpSharpNet;
using System;
using System.Collections.Generic;

namespace ErdDevice
{
    public class Erd
    {
        public string IpAddress { get; private set; }
        public string Community { get; private set; }

        private string oidMonitorVoltageSignal = ".1.3.6.1.4.1.40418.2.2.3.6.0";    //INTEGER {voltageIsYes(2),voltageIsNo(1)}
        private string oidVoltageSensorContact10 = ".1.3.6.1.4.1.40418.2.2.4.2.0";  //INTEGER
        private string oidTestFlag = ".1.3.6.1.4.1.40418.2.2.6.1.0";                //INTEGER {on(1),off(0)}
        private string oidTestMinVoltage = ".1.3.6.1.4.1.40418.2.2.6.2.0";          //UINTEGER32
        private string oidTestMaxPeriod = ".1.3.6.1.4.1.40418.2.2.6.3.0";           //INTEGER
        private string oidTestLastPeriod = ".1.3.6.1.4.1.40418.2.2.6.4.0";          //INTEGER
        private string oidRebootContact6_8 = ".1.3.6.1.4.1.40418.2.2.2.1.0";        //INTEGER {reset(1)}

        public Erd(string ipAddress, string community)
        {
            IpAddress = ipAddress;
            Community = community;
        }

        public string GetMonitorVoltageSignal()
        {
            switch (Convert.ToInt32(GetDataSnrERD(oidMonitorVoltageSignal)))
            {
                case 1: return "voltageIsNo";
                case 2: return "voltageIsYes";
                default: return "Error";
            }
        }

        public double GetVoltageSensorContact10()
        {
            return Convert.ToDouble(GetDataSnrERD(oidVoltageSensorContact10)) / 100;
        }

        public string GetTestFlag()
        {
            switch (Convert.ToInt32(GetDataSnrERD(oidTestFlag)))
            {
                case 0: return "off";
                case 1: return "on";
                default: return "Error";
            }
        }
        public void SetTestFlag(int testFlag)
        {
            SetDataSnrERD(oidTestFlag, testFlag);
        }
        public int GetTestMinVoltage()
        {
            return Convert.ToInt32(GetDataSnrERD(oidTestMinVoltage));
        }
        public void SetTestMinVoltage(int minVoltage)
        {
            SetDataSnrERD(oidTestMinVoltage, minVoltage);
        }
        public int GetTestMaxPeriod()
        {
            return Convert.ToInt32(GetDataSnrERD(oidTestMaxPeriod));
        }
        public void SetTestMaxPeriod(int maxPeriod)
        {
            SetDataSnrERD(oidTestMaxPeriod, maxPeriod);
        }
        public int GetTestLastPeriod()
        {
            return Convert.ToInt32(GetDataSnrERD(oidTestLastPeriod));
        }

        public void ErdReboot()
        {
            SetDataSnrERD(oidRebootContact6_8, 1);
        }


        private string GetDataSnrERD(string oid)
        {
            string value = "";
            var snmp = new SimpleSnmp(IpAddress, Community);
            Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver1, new[] { oid });
            if (result == null)
                return value;
            foreach (var kvp in result)
            {
                value += kvp.Value;
            }
            return value;
        }
        private void SetDataSnrERD(string oid, int data)
        {
            var snmp = new SimpleSnmp(IpAddress, Community);
            Pdu pdu = new Pdu
            {
                Type = PduType.Set
            };
            pdu.VbList.Add(new Oid(oidTestFlag), new Integer32(data));
            Dictionary<Oid, AsnType> result = snmp.Set(SnmpVersion.Ver1, pdu);
        }
    }
}
