using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Cassia;
using System.Security.Principal;

class clsRDP
{
    public List<clsRDPModel> getSessionListOnLocalhost(string[] accountExceptions=null)
    {
        List<clsRDPModel> result = new List<clsRDPModel>();
        ITerminalServicesManager manager = new TerminalServicesManager();
        //using (ITerminalServer server = manager.GetRemoteServer(serverIP))
        using (ITerminalServer server = manager.GetLocalServer())
        {
            IList<ITerminalServicesSession> sessions;
            sessions = server.GetSessions();
            server.Open();
            foreach (ITerminalServicesSession session in sessions)
            {
                NTAccount account = session.UserAccount;
                if (account != null)
                {
                    if(!getAccountIsException(accountExceptions, account.ToString()))
                    {
                        result.Add(new clsRDPModel(
                            session.ConnectionState.ToString(), 
                            session.DomainName,
                            session.UserName,
                            (session.ClientIPAddress!=null? session.ClientIPAddress.ToString():""),
                            session.ConnectTime,
                            (session.ConnectionState==ConnectionState.Active? null : session.DisconnectTime),
                            session.SessionId));
                        /*
                        if (session.ConnectionState == ConnectionState.Active)
                        {
                            result.Add(new clsRDPModel("Active", account.ToString()));
                        }
                        else
                        {
                            result.Add(new clsRDPModel("INACTIVE", account.ToString()));
                        }
                        */
                    }
                }
            }
        }
        return result;
    }
    private bool getAccountIsException(string[] accountExceptions,string account)
    {
        #region Variable
        var result = false;
        string[] accounts;
        #endregion
        #region Procedure
        try
        {
            if (accountExceptions!=null && accountExceptions.Length > 0)
            {
                accounts = account.Split(new string[] { @"\" }, StringSplitOptions.None);
                for (int i = 0; i < accountExceptions.Length; i++)
                {
                    if (accounts.Length > 1)
                    {
                        if (accountExceptions[i].ToLower().Trim() == accounts[1].ToLower().Trim())
                        {
                            result = true;
                            break;
                        }
                    }
                    else
                    {
                        if (accountExceptions[i].ToLower().Trim() == account.ToLower().Trim())
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
        }
        catch (Exception) { }
        #endregion
        return result;
    }
}
public class clsRDPModel
{
    public clsRDPModel(string Status,string Domain,string Username,string IPAddress,DateTime? ConnectTime,DateTime? DisconnectTime,int SessionID)
    {
        _status = Status;
        _domain = Domain;
        _username = Username;
        _ipAddress = IPAddress;
        _connectTime = ConnectTime;
        _disconnectTime = DisconnectTime;
        _sessionID = SessionID;
    }
    private string _status;
    public string Status
    {
        get { return _status; }
        set { _status = value; }
    }
    private string _domain;
    public string Domain
    {
        get { return _domain; }
        set { _domain = value; }
    }
    private string _username;
    public string Username
    {
        get { return _username; }
        set { _username = value; }
    }
    private string _ipAddress;
    public string IPAddress
    {
        get { return _ipAddress; }
        set { _ipAddress = value; }
    }
    private DateTime? _connectTime;
    public DateTime? ConnectTime
    {
        get { return _connectTime; }
        set { _connectTime = value; }
    }
    private DateTime? _disconnectTime;
    public DateTime? DisconnectTime
    {
        get { return _disconnectTime; }
        set { _disconnectTime = value; }
    }
    private int _sessionID;
    public int SessionID
    {
        get { return _sessionID; }
        set { _sessionID = value; }
    }
}