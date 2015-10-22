using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;

/// <summary>
/// Global Variable ตัวแปรครอบจักรวาล
/// </summary>
/// <example>
/// Response.Write(clsGlobal.ApplicationName + " : v." + clsGlobal.ApplicationVersion);
/// </example>
public static class clsGlobal
{
    private static string _applicationName="RemoteDesktopCenter";
    public static string ApplicationName
    {
        get { return _applicationName; }
        set { _applicationName = value; }
    }
    private static string _applicationVersion = VersionBuilder();
    public static string ApplicationVersion
    {
        get { return _applicationVersion; }
        set { _applicationVersion = value; }
    }
    private static clsSQL.DBType _dbType=clsSQL.DBType.MySQL;
    public static clsSQL.DBType dbType
    {
        get { return _dbType; }
        set { _dbType = value; }
    }
    private static string _cs = "cs";//or System.Configuration.ConfigurationSettings.AppSettings["cs"];
    public static string cs
    {
        get { return _cs; }
        set { _cs = value; }
    }
    static public string ExecutePathBuilder()
    {
        #region Variable
        var result = "";
        #endregion
        #region Procedure
        result = AppDomain.CurrentDomain.BaseDirectory;
        #endregion
        return result;
    }
    static public string VersionBuilder()
    {
        #region Variable
        var result = "";
        #endregion
        #region Procedure
        Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        result = version.Major.ToString() + "." + version.Minor.ToString();
        #endregion
        return result;
    }
    static public DateTime LastUpdateBuilder()
    {
        #region Variable
        string filePath = System.Reflection.Assembly.GetCallingAssembly().Location;
        const int c_PeHeaderOffset = 60;
        const int c_LinkerTimestampOffset = 8;
        byte[] b = new byte[2048];
        System.IO.Stream s = null;
        DateTime dttm = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        #endregion
        #region Procedure
        try
        {
            s = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            s.Read(b, 0, 2048);
        }
        finally
        {
            if (s != null)
            {
                s.Close();
            }
        }

        int i = System.BitConverter.ToInt32(b, c_PeHeaderOffset);
        int secondsSince1970 = System.BitConverter.ToInt32(b, i + c_LinkerTimestampOffset);
        dttm = dttm.AddSeconds(secondsSince1970);
        dttm = dttm.ToLocalTime();
        #endregion
        
        return dttm;
    }
	static public string WindowsLogonBuilder()
    {
        #region Variable
        var result = "";
        #endregion
        #region Procedure
        result = System.Environment.UserName.ToLower();
        #endregion
        return result;
    }
    static public string IPAddress()
    {
        #region Variable
        var result = "";
        var result2 = "";
        int countShow = 0;
        #endregion
        #region Procedure
        try
        {
            foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (netInterface.OperationalStatus == OperationalStatus.Up)
                {
                    IPInterfaceProperties ipProps = netInterface.GetIPProperties();
                    for (int i = 0; i < ipProps.UnicastAddresses.Count; i++)
                    {
                        UnicastIPAddressInformation addr = ipProps.UnicastAddresses[i];
                        if (addr.Address.ToString() != "127.0.0.1")
                        {
                            if (addr.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                if (addr.Address.ToString().StartsWith("10."))
                                {
                                    if (result.Length > 0) result += ",";
                                    //result += System.Environment.NewLine;
                                    result += addr.Address.ToString();
                                    countShow += 1;
                                }
                                else
                                {
                                    result2 += addr.Address.ToString();
                                    //result2 += System.Environment.NewLine;
                                }

                            }
                        }
                    }
                }
            }
        }
        catch (Exception) { }
        if (countShow == 0)
        {
            result = result2;
        }
        #endregion
        return result;
    }
    static public string HostNameBuilder()
    {
        #region Variable
        var result = "";
        #endregion
        #region Procedure
        try
        {
            result = System.Environment.MachineName;
        }
        catch (Exception) { }
        #endregion
        return result;
    }
}