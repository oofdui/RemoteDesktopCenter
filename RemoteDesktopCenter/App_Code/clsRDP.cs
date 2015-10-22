using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Runtime.InteropServices;

public class clsRDP
{
    public int getSessionInfo(string serverIP,out string outMessage)
    {
        #region Variable
        string computer = @"\\" + serverIP + @"\";
        outMessage = string.Empty;
        #endregion
        try
        {
            ConnectionOptions co = new ConnectionOptions();
            co.Impersonation = ImpersonationLevel.Impersonate;
            co.Authentication = AuthenticationLevel.Packet;
            co.Timeout = new TimeSpan(0, 0, 30);
            co.EnablePrivileges = true;
            co.Username = @"BDMS\nithi.re";
            co.Password = "Offjun10r";

            ManagementPath mp = new ManagementPath();
            mp.NamespacePath = @"\root\cimv2";
            mp.Server = serverIP;               ///Regard this!!!!

            ManagementScope ms = new ManagementScope(mp, co);
            ms.Connect();

            ManagementObjectSearcher searcher;
            searcher = new ManagementObjectSearcher
            (
                //ms, new ObjectQuery("select * from Win32_DisplayConfiguration")
                ms, new ObjectQuery(computer + @"root\CIMV2", "SELECT * FROM Win32_TerminalService")
                //ms, new ObjectQuery(computer + @"root\CIMV2", "select * from Win32_DisplayConfiguration")
            );
            //ManagementObjectSearcher searcher = new ManagementObjectSearcher(computer + @"root\CIMV2", "SELECT * FROM Win32_TerminalService");
            ManagementObjectCollection colItems = searcher.Get();
            //Console.WriteLine("{0} instance{1}", colItems.Count, (colItems.Count == 1 ? String.Empty : "s"));
            //Console.WriteLine();

            foreach (ManagementObject queryObj in colItems)
            {
                outMessage += string.Format("AcceptPause             : {0}" + Environment.NewLine, queryObj["AcceptPause"]);
                outMessage += string.Format("AcceptStop              : {0}" + Environment.NewLine, queryObj["AcceptStop"]);
                outMessage += string.Format("Caption                 : {0}" + Environment.NewLine, queryObj["Caption"]);
                outMessage += string.Format("CheckPoint              : {0}" + Environment.NewLine, queryObj["CheckPoint"]);
                outMessage += string.Format("CreationClassName       : {0}" + Environment.NewLine, queryObj["CreationClassName"]);
                outMessage += string.Format("Description             : {0}" + Environment.NewLine, queryObj["Description"]);
                outMessage += string.Format("DesktopInteract         : {0}" + Environment.NewLine, queryObj["DesktopInteract"]);
                outMessage += string.Format("DisconnectedSessions    : {0}" + Environment.NewLine, queryObj["DisconnectedSessions"]);
                outMessage += string.Format("DisplayName             : {0}" + Environment.NewLine, queryObj["DisplayName"]);
                outMessage += string.Format("ErrorControl            : {0}" + Environment.NewLine, queryObj["ErrorControl"]);
                outMessage += string.Format("ExitCode                : {0}" + Environment.NewLine, queryObj["ExitCode"]);
                outMessage += string.Format("InstallDate             : {0}" + Environment.NewLine, queryObj["InstallDate"]);
                outMessage += string.Format("Name                    : {0}" + Environment.NewLine, queryObj["Name"]);
                outMessage += string.Format("PathName                : {0}" + Environment.NewLine, queryObj["PathName"]);
                outMessage += string.Format("ProcessId               : {0}" + Environment.NewLine, queryObj["ProcessId"]);
                outMessage += string.Format("ServiceSpecificExitCode : {0}" + Environment.NewLine, queryObj["ServiceSpecificExitCode"]);
                outMessage += string.Format("ServiceType             : {0}" + Environment.NewLine, queryObj["ServiceType"]);
                outMessage += string.Format("Started                 : {0}" + Environment.NewLine, queryObj["Started"]);
                outMessage += string.Format("StartMode               : {0}" + Environment.NewLine, queryObj["StartMode"]);
                outMessage += string.Format("StartName               : {0}" + Environment.NewLine, queryObj["StartName"]);
                outMessage += string.Format("State                   : {0}" + Environment.NewLine, queryObj["State"]);
                outMessage += string.Format("Status                  : {0}" + Environment.NewLine, queryObj["Status"]);
                outMessage += string.Format("SystemCreationClassName : {0}" + Environment.NewLine, queryObj["SystemCreationClassName"]);
                outMessage += string.Format("SystemName              : {0}" + Environment.NewLine, queryObj["SystemName"]);
                outMessage += string.Format("TagId                   : {0}" + Environment.NewLine, queryObj["TagId"]);
                outMessage += string.Format("TotalSessions           : {0}" + Environment.NewLine, queryObj["TotalSessions"]);
                outMessage += string.Format("WaitHint                : {0}" + Environment.NewLine, queryObj["WaitHint"]);
            }
            return 0;
        }
        catch (Exception e)
        {
            Console.Error.WriteLine("An error occurred while querying WMI: {0}", e.Message);
            return 1;
        }
    }
}