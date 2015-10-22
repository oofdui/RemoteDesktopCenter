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
        #region GlobalVariable
        int refreshSecond = int.Parse(System.Configuration.ConfigurationManager.AppSettings["refreshSecond"]);
        int refreshSecondCount = 0;
        #endregion
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
            #endregion
            #region Procedure
            lblHeader.Text = clsGlobal.ApplicationName+" v."+clsGlobal.ApplicationVersion;
            lblSubHeader.Text = "ศูนย์รวมบริหารและจัดการเซิฟเวอร์สำหรับรีโมท";
            lblFooterDetail.Text = string.Format("Server : {0}", clsSQL.getAppSettingServerName(clsGlobal.cs));
            ttDefault.SetToolTip(btMinimize, "ย่อหน้าต่าง");
            ttDefault.SetToolTip(btClose, "ออกจากโปรแกรม");
            txtUsername.Text = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName+@"\"+clsGlobal.WindowsLogonBuilder();
            if (clsGlobal.ServerMode)
            {
                txtUsername.Enabled = false;txtPassword.Enabled = false;
                tmDefault.Enabled = true;
                tmDefault.Start();
            }
            #endregion
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
        private void setSessionList()
        {
            #region Variable
            var clsSQL = new clsSQL();
            var clsRDP = new clsRDP();
            #endregion
            #region Procedure
            List<clsRDPModel> clsRDPModels = new List<clsRDPModel>();
            clsRDPModels = clsRDP.getSessionListOnLocalhost(System.Configuration.ConfigurationManager.AppSettings["exceptionUsername"].Split(','));
            lvSession.Items.Clear();
            if (clsRDPModels.Count > 0)
            {
                #region Insert || Update
                setSessionLog(clsRDPModels);
                for (int i = 0; i < clsRDPModels.Count; i++)
                {
                    ListViewBuilder(lvSession, null, 99,
                        clsRDPModels[i].Status,
                        clsRDPModels[i].Domain,
                        clsRDPModels[i].Username,
                        clsRDPModels[i].IPAddress,
                        clsRDPModels[i].ConnectTime.Value.ToString("dd/MM/yyyy HH:mm"),
                        (clsRDPModels[i].DisconnectTime != null ? clsRDPModels[i].DisconnectTime.Value.ToString("dd/MM/yyyy HH:mm") : ""),
                        clsRDPModels[i].SessionID.ToString());
                }
                #endregion
            }
            else
            {
                ListViewResizeColumn(lvSession, 99);
            }
            #endregion
        }
        private bool setSessionLog(List<clsRDPModel> clsRDPModels)
        {
            #region Variable
            var result = false;
            var strSQL = "";
            var dt = new DataTable();
            var clsSQL = new clsSQL(clsGlobal.dbType, clsGlobal.cs);
            #endregion
            #region Procedure
            try
            {
                dt = clsSQL.Bind("SELECT UID,Domain,Username,SessionID,ConnectWhen,DisconnectWhen,Status FROM sessionlog WHERE ServerName='" + clsGlobal.IPAddress() + "' AND DisconnectWhen IS NULL AND StatusFlag='A';");
                if (dt != null && dt.Rows.Count > 0)
                {
                    #region Logout
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        var exist = false;
                        try {
                            for (int j = 0; j < clsRDPModels.Count; j++)
                            {
                                try {
                                    if (dt.Rows[i]["Domain"].ToString() == clsRDPModels[j].Domain &&
                                        dt.Rows[i]["Username"].ToString() == clsRDPModels[j].Username &&
                                        dt.Rows[i]["SessionID"].ToString() == clsRDPModels[j].SessionID.ToString())
                                    {
                                        exist = true;
                                        break;
                                    }
                                }
                                catch (Exception exSelect)
                                {
                                    MessageBox.Show("Fail on condition." + Environment.NewLine + exSelect.Message, "setSessionLog", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    tmDefault.Stop(); tmDefault.Enabled = false; return false;
                                }
                            }
                            if (!exist)
                            {
                                var sql = "UPDATE sessionlog SET DisconnectWhen=NOW(),Status='Logoff' WHERE UID=" + dt.Rows[i]["UID"].ToString();
                                if (!clsSQL.Execute(sql))
                                {
                                    MessageBox.Show("Can't update sessionlog." + Environment.NewLine, "setSessionLog", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        catch (Exception exSelect)
                        {
                            MessageBox.Show("Fail on update(139)." + Environment.NewLine + exSelect.Message, "setSessionLog", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tmDefault.Stop(); tmDefault.Enabled = false; return false;
                        }
                    }
                    #endregion
                }
                #region Connected
                for (int i = 0; i < clsRDPModels.Count; i++)
                {
                    try
                    {
                        strSQL = "SELECT UID,IFNULL(DisconnectWhen,'')DisconnectWhen,Status " +
                        "FROM sessionlog " +
                        "WHERE ServerName='" + clsGlobal.IPAddress() + "' " +
                        "AND Domain='" + clsRDPModels[i].Domain + "' " +
                        "AND Username='" + clsRDPModels[i].Username + "' " +
                        "AND SessionID='" + clsRDPModels[i].SessionID + "' " +
                        "AND ConnectWhen='" + clsRDPModels[i].ConnectTime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                        "AND StatusFlag='A';";
                    }
                    catch(Exception exSelect)
                    {
                        MessageBox.Show("Fail on select." + Environment.NewLine + exSelect.Message, "setSessionLog", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tmDefault.Stop(); tmDefault.Enabled = false; return false;
                    }
                    var dtUpdate = new DataTable();
                    var sql = ""; var addSeparate = false;
                    dtUpdate = clsSQL.Bind(strSQL);
                    if (dtUpdate != null && dtUpdate.Rows.Count > 0)
                    {
                        try
                        {
                            sql = "UPDATE sessionlog SET ";
                            if (dtUpdate.Rows[0]["Status"].ToString() != clsRDPModels[i].Status)
                            {
                                sql += "Status='" + clsRDPModels[i].Status + "' ";
                                addSeparate = true;
                            }
                            if (dtUpdate.Rows[0]["DisconnectWhen"].ToString() != (clsRDPModels[i].DisconnectTime != null ? clsRDPModels[i].DisconnectTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : ""))
                            {
                                if (addSeparate) sql += ",";
                                sql += "DisconnectWhen=" + (clsRDPModels[i].DisconnectTime != null ? "'"+clsRDPModels[i].DisconnectTime.Value.ToString("yyyy-MM-dd HH:mm:ss")+"'" : "NULL") + " ";
                                addSeparate = true;
                            }
                            sql += "WHERE UID=" + dtUpdate.Rows[0]["UID"].ToString();
                            if (!addSeparate) sql = "";
                        }
                        catch (Exception exSelect)
                        {
                            MessageBox.Show("Fail on update(188)." + Environment.NewLine + exSelect.Message, "setSessionLog", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tmDefault.Stop(); tmDefault.Enabled = false; return false;
                        }
                    }
                    else
                    {
                        try {
                            sql = "INSERT INTO sessionlog(ServerName,Domain,Username,IPAddress,SessionID,Status,ConnectWhen,DisconnectWhen,StatusFlag) " +
                                "VALUES('" + clsGlobal.IPAddress() + "'," +
                                "'" + clsRDPModels[i].Domain + "'," +
                                "'" + clsRDPModels[i].Username + "'," +
                                "'" + clsRDPModels[i].IPAddress + "'," +
                                "" + clsRDPModels[i].SessionID.ToString() + "," +
                                "'" + clsRDPModels[i].Status + "'," +
                                "'" + clsRDPModels[i].ConnectTime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                "" + (clsRDPModels[i].DisconnectTime != null ? "'" + clsRDPModels[i].DisconnectTime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'" : "NULL") + "," +
                                "'A');";
                        }
                        catch (Exception exSelect)
                        {
                            MessageBox.Show("Fail on insert." + Environment.NewLine + exSelect.Message, "setSessionLog", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tmDefault.Stop(); tmDefault.Enabled = false; return false;
                        }
                    }
                    if (sql != "")
                    {
                        if (!clsSQL.Execute(sql))
                        {
                            MessageBox.Show("Can't update sessionlog." + Environment.NewLine + sql, "setSessionLog", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tmDefault.Stop(); tmDefault.Enabled = false; return false;
                        }
                    }
                    //if (clsRDPModels[i].DisconnectTime== null)
                    //{ 
                    //    var getExistCheckerResult = "";
                    //    getExistCheckerResult = getExistChecker(dt, clsRDPModels[i]);
                    //    if (getExistCheckerResult != "")
                    //    {
                    //        if (!clsSQL.Execute(getExistCheckerResult))
                    //        {
                    //            MessageBox.Show("Can't update sessionlog." + Environment.NewLine + getExistCheckerResult, "setSessionLog", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //        }
                    //    }
                    //}
                }
                #endregion
            }
            catch(Exception ex)
            {
                MessageBox.Show("Fail something on setSessionlog." + Environment.NewLine + ex.Message, "setSessionLog", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tmDefault.Stop(); tmDefault.Enabled = false; return false; 
            }
            #endregion
            return result;
        }
        private string getExistChecker(DataTable dt,clsRDPModel clsRDPModel)
        {
            #region Variable
            var result = "";
            #endregion
            #region Procedure
            if(dt!=null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //txtDebug.Text = "Domain : " + dt.Rows[i]["Domain"].ToString() + " | " + clsRDPModel.Domain + Environment.NewLine +
                    //    "Username : " + dt.Rows[i]["Username"].ToString() + " | " + clsRDPModel.Username + Environment.NewLine +
                    //    "SessionID : " + dt.Rows[i]["SessionID"].ToString() + " | " + clsRDPModel.SessionID + Environment.NewLine +
                    //    "ConnectWhen : " + dt.Rows[i]["ConnectWhen"].ToString() + " | " + clsRDPModel.ConnectTime.Value.ToString() + Environment.NewLine +
                    //    "Status : " + dt.Rows[i]["Status"].ToString() + " | " + clsRDPModel.Status + Environment.NewLine;
                    if (dt.Rows[i]["Domain"].ToString() == clsRDPModel.Domain && 
                        dt.Rows[i]["Username"].ToString() == clsRDPModel.Username &&
                        dt.Rows[i]["SessionID"].ToString() == clsRDPModel.SessionID.ToString() &&
                        DateTime.Parse(dt.Rows[i]["ConnectWhen"].ToString()).ToString() == clsRDPModel.ConnectTime.ToString() &&
                        dt.Rows[i]["Status"].ToString() == clsRDPModel.Status)
                    {
                        result = "";
                        break;
                    }
                    else if (dt.Rows[i]["Domain"].ToString() == clsRDPModel.Domain && 
                            dt.Rows[i]["Username"].ToString() == clsRDPModel.Username &&
                            dt.Rows[i]["SessionID"].ToString() == clsRDPModel.SessionID.ToString() &&
                            DateTime.Parse(dt.Rows[i]["ConnectWhen"].ToString()).ToString() == clsRDPModel.ConnectTime.ToString())
                    {
                        result = "UPDATE sessionlog SET " +
                            "Status='" + clsRDPModel.Status + "'" +
                            (clsRDPModel.DisconnectTime != null ? ",DisconnectWhen='" + clsRDPModel.DisconnectTime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'" : "") +
                            " WHERE UID=" + dt.Rows[i]["UID"].ToString();
                        break;
                    }
                    else
                    {
                        result = "INSERT INTO sessionlog(ServerName,Domain,Username,IPAddress,SessionID,Status,ConnectWhen,DisconnectWhen,StatusFlag) " +
                            "VALUES('" + clsGlobal.IPAddress() + "'," +
                            "'" + clsRDPModel.Domain + "'," +
                            "'" + clsRDPModel.Username.Replace(@"\",@"\\") + "'," +
                            "'" + clsRDPModel.IPAddress + "'," +
                            "" + clsRDPModel.SessionID.ToString() + "," +
                            "'" + clsRDPModel.Status + "'," +
                            "'" + clsRDPModel.ConnectTime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "" + (clsRDPModel.DisconnectTime != null ? "'" + clsRDPModel.DisconnectTime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'" : "NULL") + "," +
                            "'A');";
                        break;
                    }
                }
            }
            else
            {
                result = "INSERT INTO sessionlog(ServerName,Domain,Username,IPAddress,SessionID,Status,ConnectWhen,DisconnectWhen,StatusFlag) " +
                    "VALUES('" + clsGlobal.IPAddress() + "'," +
                    "'" + clsRDPModel.Domain + "'," +
                    "'" + clsRDPModel.Username + "'," +
                    "'" + clsRDPModel.IPAddress + "'," +
                    "" + clsRDPModel.SessionID.ToString() + "," +
                    "'" + clsRDPModel.Status + "'," +
                    "'" + clsRDPModel.ConnectTime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "" + (clsRDPModel.DisconnectTime != null ? "'" + clsRDPModel.DisconnectTime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'" : "NULL") + "," +
                    "'A');";
            }
            #endregion
            return result;
        }
        public void ListViewBuilder(ListView listView, Color? color = null, int columnFullWidth = 99, params string[] value)
        {
            listView.Items.Add(new ListViewItem(value));
            if (color != null)
            {
                listView.Items[listView.Items.Count - 1].ForeColor = (Color)color;
            }
            listView.EnsureVisible(listView.Items.Count - 1);
            ListViewResizeColumn(listView, columnFullWidth);
        }
        private void ListViewResizeColumn(ListView listView, int column)
        {
            var totalColumnWidth = 0;
            var calculateColumnWidth = 0;
            for (int i = 0; i < listView.Columns.Count; i++)
            {
                if (column == 99)
                {
                    if (i < listView.Columns.Count - 1)
                    {
                        listView.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent);
                    }
                }
                else
                {
                    if (column != i)
                    {
                        listView.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent);
                        totalColumnWidth += listView.Columns[i].Width;
                    }
                }
            }
            #region FullFill
            if (column == 99)
            {
                listView.Columns[listView.Columns.Count - 1].Width = -2;
            }
            else
            {
                calculateColumnWidth = listView.Width - totalColumnWidth - (listView.Width / 10);//ลบด้วย 10% ของความกว้างทั้งหมดอีกรอบกันเลย
                listView.Columns[column].Width = calculateColumnWidth;
                listView.Columns[listView.Columns.Count - 1].Width = -2;
            }
            #endregion
        }
        private void tmDefault_Tick(object sender, EventArgs e)
        {
            if (refreshSecondCount == 0)
            {
                setSessionList();
            }
            else if(refreshSecondCount >= refreshSecond)
            {
                refreshSecondCount = -1;
            }
            refreshSecondCount += 1;
        }
        private void btMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void btClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
