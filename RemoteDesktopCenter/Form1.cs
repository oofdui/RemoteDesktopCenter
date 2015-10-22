using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RemoteDesktopCenter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            setDefault();
        }
        private void setDefault()
        {
            #region Variable
            var clsSQL = new clsSQL();
            var clsRDP = new clsRDP();
            #endregion
            #region Procedure
            lblHeader.Text = clsGlobal.ApplicationName+" v."+clsGlobal.ApplicationVersion;
            lblSubHeader.Text = "ศูนย์รวมบริหารและจัดการเซิฟเวอร์สำหรับรีโมท";
            lblFooterDetail.Text = string.Format("Server : {0}", clsSQL.getAppSettingServerName(clsGlobal.cs));
            ttDefault.SetToolTip(btMinimize, "ย่อหน้าต่าง");
            ttDefault.SetToolTip(btClose, "ออกจากโปรแกรม");
            txtUsername.Text = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName+@"\"+clsGlobal.WindowsLogonBuilder();
            var outMessage = "";
            //clsRDP.getSessionInfo("10.121.10.165",out outMessage);
            List<TerminalSessionData> terminalSessionData = new List<TerminalSessionData>();
            //terminalSessionData=clsRDP.ListSessions("10.121.10.165");
            TermServicesManager TermServicesManager = new TermServicesManager();
            terminalSessionData = TermServicesManager.ListSessions("BRH-RDC01.BDMS.CO.TH");
            txtRDPDetail.Text = outMessage;
            #endregion
        }

        private void btMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void btClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var serverIP = "10.121.10.165";
            Process rdcProcess = new Process();
            rdcProcess.StartInfo.FileName = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\system32\cmdkey.exe");
            rdcProcess.StartInfo.Arguments = "/generic:TERMSRV/"+ serverIP + " /user:" + txtUsername.Text.Trim() + " /pass:" + txtPassword.Text.Trim();
            rdcProcess.Start();

            rdcProcess.StartInfo.FileName = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\system32\mstsc.exe");
            rdcProcess.StartInfo.Arguments = "/v " + serverIP;
            rdcProcess.Start();
        }
    }
}
