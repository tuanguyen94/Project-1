using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.IO;
using Microsoft.Win32;


namespace Project1_final
{
    public class function 
    {

        

        /// <summary>
        /// Phương thức hỗ trợ cấp địa chỉ IP động
        /// không phải thiết lập bằng tay
        /// </summary>
        public void SetDHCP()
        {
            //Khởi tạo một lớp ManagementClass
            //ManagementClas là 1 class có các phương thức lấy và cài đặt thông tin của thiết bị
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            //Khai báo 1 tập hợp các thể hiện của lớp ManagementClass được trả về từ phương thức GetInstances()
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                //Kiểm tra xem máy tính có cho phép thiết đặt địa chỉ IP không
                //If TRUE, TCP/IP is bound and enabled on this network adapter.
                if ((bool)mo["IPEnabled"])
                {
                    
                        //GetMethodParameters Trả về một  1 element cơ bản đại diện cho danh sách các thông số đầu vào của phương pháp.
                        ManagementBaseObject newDNS = mo.GetMethodParameters("SetDNSServerSearchOrder");
                        newDNS["DNSServerSearchOrder"] = null;
                        ManagementBaseObject enableDHCP = mo.InvokeMethod("EnableDHCP", null, null);
                        ManagementBaseObject setDNS = mo.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);
                    
                }
            }
        }


        /// <summary>
        /// Thiết lập địa chỉ IP, subnet, gateway, dns, hostname dựa trên các thông số đã nhập vào
        /// </summary>
        /// <param name="IpAddresses">Địa chỉ IP mới của máy</param>
        /// <param name="SubnetMask">Địa chỉ Subnet mới của máy</param>
        /// <param name="Gateway">Default gateway</param>
        /// <param name="DnsSearchOrder">Dns mới</param>
        /// <param name="Hostname">Hostname mới</param>
        public void SetIP( string IpAddresses, string SubnetMask, string Gateway, string DnsSearchOrder, string Hostname)
        {
            //Khởi tạo một lớp ManagementClass
            //ManagementClas là 1 class có các phương thức lấy và cài đặt thông tin của thiết bị
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            //Khai báo 1 tập hợp các thể hiện của lớp ManagementClass được trả về từ phương thức GetInstances()
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {

                //Kiểm tra xem máy tính có cho phép thiết đặt địa chỉ IP không
                //If TRUE, TCP/IP is bound and enabled on this network adapter.
                if ((bool)mo["IPEnabled"])
                {
                    

                        ManagementBaseObject newIP = mo.GetMethodParameters("EnableStatic");
                        ManagementBaseObject newGate = mo.GetMethodParameters("SetGateways");
                        ManagementBaseObject newDNS = mo.GetMethodParameters("SetDNSServerSearchOrder");
                        

                        newGate["DefaultIPGateway"] = new string[] { Gateway };
                        newGate["GatewayCostMetric"] = new int[] { 1 };

                        newIP["IPAddress"] = IpAddresses.Split(',');
                        newIP["SubnetMask"] = new string[] { SubnetMask };

                        newDNS["DNSServerSearchOrder"] = DnsSearchOrder.Split(',');


                        ///Gọi một phương thức trên đối tượng WMI
                        //Các thông số đầu vào và đầu ra được biểu diễn là đối tượng ManagementBaseObject 
                        ManagementBaseObject setIP = mo.InvokeMethod("EnableStatic", newIP, null);
                        ManagementBaseObject setGateways = mo.InvokeMethod("SetGateways", newGate, null);
                        ManagementBaseObject setDNS = mo.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);
                        //khai báo cơ sở dữ liệu dùng để lưu trữ thông số kỹ thuật của Windows.
                        RegistryKey key = Registry.LocalMachine.OpenSubKey("SYSTEM", true).OpenSubKey("CurrentControlSet", true).OpenSubKey("Services", true).OpenSubKey("tcpip", true).OpenSubKey("Parameters", true);

                        key.SetValue("Hostname", Hostname);
                        key.SetValue("NV Hostname", Hostname);
                        
                        
                        
                        break;
                    
                }
            }
        }


        /// <summary>
        /// trả về địa chỉ IP, subnet, gateway,dns, hostname hiện tại của máy
        /// </summary>
        /// <param name="ipAdresses">Địa chỉ IP</param>
        /// <param name="subnets">Subnet</param>
        /// <param name="gateways">Default Gateway</param>
        /// <param name="dnses">Dns</param>
        /// <param name="hostname">Hostname</param>
        public void GetIP( out string[] ipAdresses, out string[] subnets, out string[] gateways, out string[] dnses, out string hostname)
        {
            ipAdresses = new string[1];
            ipAdresses[0] = "";
            subnets = new string[1];
            subnets[0] = "";
            gateways = new string[1];
            gateways[0] = "";
            dnses = new string[1];
            dnses[0] = "";
            hostname = "";

            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                // Make sure this is a IP enabled device. Not something like memory card or VM Ware
                if ((bool)mo["IPEnabled"])
                {
                    if (mo["IPAddress"] != null)
                    {

                        ipAdresses = (string[])mo["IPAddress"];
                        subnets = (string[])mo["IPSubnet"];
                        gateways = (string[])mo["DefaultIPGateway"];
                        dnses = (string[])mo["DNSServerSearchOrder"];
                        hostname = Dns.GetHostName();


                    }
                       
                }
            }
        }


        

        
        
    }
}
