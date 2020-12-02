using System;
using System.Diagnostics;
using System.Management;
using System.Text;

namespace SPPC.Licensing.Local.Persistence
{
    public static class HardwareKey
    {
        public static string GetProcessorId()
        {
            string cpuId = String.Empty;
            try
            {
                ManagementClass mgmtClass = new ManagementClass("Win32_Processor");
                ManagementObjectCollection mgmtColl = mgmtClass.GetInstances();
                foreach (ManagementObject mgmtObj in mgmtColl)
                {
                    // Get only the first CPU's ID
                    cpuId = mgmtObj.Properties["ProcessorID"].Value.ToString();
                    break;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("{0}WARNING: Could not read processor ID.{0}Error : {1}{0}{0}",
                    Environment.NewLine, e.Message);
            }

            return cpuId;
        }

        public static string GetVolumeSerial()
        {
            string serialNo = String.Empty;
            try
            {
                using (ManagementObject disk = new ManagementObject(@"win32_logicaldisk.deviceid='C:'"))
                {
                    disk.Get();
                    serialNo = disk["VolumeSerialNumber"].ToString();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("{0}WARNING: Could not read serial number of drive C.{0}Error : {1}{0}{0}",
                    Environment.NewLine, e.Message);
            }

            return serialNo;
        }

        public static string GetMainBoardSerial()
        {
            string serialNo = String.Empty;
            try
            {
                var mgmtClass = new ManagementClass("Win32_BaseBoard");
                var mgmtColl = mgmtClass.GetInstances();
                foreach (ManagementObject mgmtObj in mgmtColl)
                {
                    // Get only the first motherboard
                    serialNo = mgmtObj.Properties["SerialNumber"].Value.ToString();
                    break;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("{0}WARNING: Could not read serial number of mainboard.{0}Error : {1}{0}{0}",
                    Environment.NewLine, e.Message);
            }

            return serialNo;

        }

        public static string GetFirstMacAddress()
        {
            string macAddress = String.Empty;
            try
            {
                string command = "SELECT MACAddress FROM Win32_NetworkAdapter";
                var searcher = new ManagementObjectSearcher(command);
                var collection = searcher.Get();
                foreach (ManagementObject item in collection)
                {
                    var mac = item["MACAddress"];
                    macAddress = mac?.ToString();
                    break;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("{0}WARNING: Could not read first MAC address.{0}Error : {1}{0}{0}",
                    Environment.NewLine, e.Message);
            }

            return macAddress;
        }

        public static string GetSystemUniqueId()
        {
            string uniqueId = String.Empty;
            var items = new string[]
            {
                GetProcessorId(), GetVolumeSerial(), GetMainBoardSerial(), GetFirstMacAddress()
            };

            if (items.Length > 0)
            {
                uniqueId = Convert.ToBase64String(
                    Encoding.UTF8.GetBytes(
                        String.Join("$", items)));
            }

            return uniqueId;
        }
    }
}
