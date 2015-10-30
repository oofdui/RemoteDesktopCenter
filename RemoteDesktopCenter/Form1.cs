using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace RemoteDesktopCenter
{
    public partial class Form1 : Form
    {
        #region GlobalVariable
        int refreshSecond = int.Parse(System.Configuration.ConfigurationManager.AppSettings["refreshSecond"]);
        int refreshSecondCount = 0;
        int maxActiveNumber = int.Parse(System.Configuration.ConfigurationManager.AppSettings["maxActiveNumber"]);

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        #endregion
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            setUsageLog();
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
            ttDefault.SetToolTip(btMove, "ย้ายตำแหน่งหน้าต่าง");
            ttDefault.SetToolTip(btReport, "ดูรายงาน");
            if (!getAdminChecker()) btReport.Visible = false;
            txtUsername.Text = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName+@"\"+clsGlobal.WindowsLogonBuilder();
            var savePassword = System.Configuration.ConfigurationManager.AppSettings["savePassword"];
            if (savePassword.Trim() != "")
            {
                var clsSecurity = new clsSecurity();
                txtPassword.Text = clsSecurity.Decrypt(savePassword);
            }
            if (clsGlobal.ServerMode)
            {
                try
                {
                    txtUsername.Enabled = false; txtPassword.Enabled = false;
                    lvServerList.Visible = false;
                    tbContent.RowStyles[1].Height = 0;
                    tmDefault.Enabled = true;
                    tmDefault.Start();
                }
                catch(Exception ex)
                {
                    try
                    {
                        wsDefault.ServiceSoapClient wsDefault = new wsDefault.ServiceSoapClient();
                        wsDefault.MailSend(System.Configuration.ConfigurationManager.AppSettings["mailTo"],
                            clsGlobal.ApplicationName,
                            "<h2>" + clsGlobal.IPAddress() + "</h2>" + ex.Message,
                            "AutoSystem@glsict.com",
                            System.Configuration.ConfigurationManager.AppSettings["siteCode"] + " : " + clsGlobal.ApplicationName,
                            "", "", "", false);
                    }
                    catch (Exception) { }
                }
            }
            else
            {
                lvSession.Visible = false;
                tbContent.RowStyles[0].Height = 0;
                tmClient.Enabled = true;
                tmClient.Start();
            }
            #endregion
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
                setSessionLog(clsRDPModels);
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
        private void btMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void btClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void setUsageLog()
        {
            if (System.Configuration.ConfigurationManager.AppSettings["enableUsageLog"].ToLower().Trim() == "true")
            {
                #region Variable
                var wsCenter = new wsCenter.ServiceSoapClient();
                #endregion
                #region Procedure
                try
                {
                    wsCenter.InsertLogApplicationBySite(
                        clsGlobal.ApplicationName,
                        (clsGlobal.ServerMode ? "ServerMode" : "ClientMode"),
                        System.Configuration.ConfigurationManager.AppSettings["siteCode"],
                        clsGlobal.WindowsLogonBuilder(),
                        clsGlobal.IPAddress(),
                        clsGlobal.HostNameBuilder());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดขณะ setUsageLog()" + Environment.NewLine + ex.Message, "Error on setUsageLog", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                #endregion
            }
        }
        private void setSavePassword()
        {
            if (txtPassword.Text.Trim() != "")
            {
                var clsData = new clsDataNative();
                var clsSecurity = new clsSecurity();
                clsData.AppConfigUpdater("savePassword", clsSecurity.Encrypt(txtPassword.Text.Trim()));
            }
        }
        private void getServerList()
        {
            #region Variable
            var clsSQL = new clsSQL(clsGlobal.dbType, clsGlobal.cs);
            var dt = new DataTable();
            var strSQL = new StringBuilder();
            List<string> imageDetails = new List<string>();
            #endregion
            #region Procedure
            lvServerList.Items.Clear();
            #region SQLQuery
            strSQL.Append("SELECT ");
            strSQL.Append("A.Name,A.IPAddress,(SELECT COUNT(UID) FROM sessionlog WHERE (ServerName = A.IPAddress OR ServerName = A.Name) AND StatusFlag = 'A' AND DisconnectWhen IS NULL) ActiveCount ");
            strSQL.Append("FROM ");
            strSQL.Append("serverlist A ");
            strSQL.Append("WHERE ");
            strSQL.Append("A.StatusFlag = 'A' ");
            strSQL.Append("ORDER BY ");
            strSQL.Append("A.Sort;");
            #endregion
            dt = clsSQL.Bind(strSQL.ToString());
            if(dt!=null && dt.Rows.Count > 0)
            {
                var imlDefault = new ImageList();
                var imageName = "";
                var imagePath = AppDomain.CurrentDomain.BaseDirectory + @"Resource\";
                imlDefault.ImageSize = new Size(64, 64);

                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ActiveCount"].ToString() == "0")
                    {
                        imageName = "icServerFree.png";
                        imageDetails.Add(dt.Rows[i]["Name"].ToString()+Environment.NewLine+"ว่าง");
                    }
                    else
                    {
                        if(int.Parse(dt.Rows[i]["ActiveCount"].ToString()) >= maxActiveNumber)
                        {
                            imageName = "icServerFull.png";
                            imageDetails.Add(dt.Rows[i]["Name"].ToString() + Environment.NewLine + "เต็ม");
                        }
                        else
                        {
                            imageName = "icServerBusy.png";
                            imageDetails.Add(dt.Rows[i]["Name"].ToString() + Environment.NewLine + "ว่างบางส่วน");
                        }
                    }

                    imlDefault.Images.Add(
                        Image.FromFile(imagePath + imageName)
                    );
                }

                lvServerList.LargeImageList = imlDefault;
                for(int i = 0; i < imageDetails.Count; i++)
                {
                    lvServerList.Items.Add(imageDetails[i], i);
                }
            }
            #endregion
        }
        private void lvServerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text.Trim() != "")
            {
                setSavePassword();
                foreach (ListViewItem lvi in lvServerList.SelectedItems)
                {
                    var items = lvi.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    if (items.Length >= 2)
                    {
                        if (items[1].Contains("เต็ม"))
                        {
                            MessageBox.Show("มีผู้ใช้งานเต็มจำนวน โปรดเลือกเซิฟเวอร์อื่น", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            var serverName = items[0];
                            var fi = new FileInfo(clsGlobal.ExecutePathBuilder() + @"Resource\connect.rdp");
                            try
                            {
                                if (fi.Exists) fi.Delete();
                                var sw = new StreamWriter(fi.FullName, true, System.Text.Encoding.UTF8);
                                var strValue = new StringBuilder();
                                strValue.Append(System.Configuration.ConfigurationManager.AppSettings["rdpParameter"].Replace("[serverName]", serverName));
                                sw.WriteLine(strValue.ToString());
                                strValue.Length = 0; strValue.Capacity = 0;
                                sw.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("เกิดข้อผิดพลาดขณะสร้าง RemoteProfile" + Environment.NewLine + ex.Message,
                                    "Create Remote Profile",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                return;
                            }
                            try
                            {
                                Process rdcProcess = new Process();
                                rdcProcess.StartInfo.FileName = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\system32\cmdkey.exe");
                                rdcProcess.StartInfo.Arguments = "/generic:TERMSRV/" + serverName + " /user:" + txtUsername.Text.Trim() + " /pass:" + txtPassword.Text.Trim();
                                rdcProcess.Start();

                                rdcProcess.StartInfo.FileName = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\system32\mstsc.exe");
                                //rdcProcess.StartInfo.Arguments = "/f /v " + serverName + "";
                                rdcProcess.StartInfo.Arguments = fi.FullName;
                                rdcProcess.Start();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("เกิดข้อผิดพลาดขณะเรียก mstsc.exe" + Environment.NewLine + ex.Message,
                                    "Call mstsc.exe",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    break;
                }
            }
            else
            {
                txtPassword.Select();
                MessageBox.Show("โปรดกรอก Password ก่อน", "Password require.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
        private void tmDefault_Tick(object sender, EventArgs e)
        {
            try
            {
                if (refreshSecondCount == 0)
                {
                    setSessionList();
                }
                else if (refreshSecondCount >= refreshSecond)
                {
                    refreshSecondCount = -1;
                }
                refreshSecondCount += 1;
            }
            catch(Exception ex)
            {
                try
                {
                    wsDefault.ServiceSoapClient wsDefault = new wsDefault.ServiceSoapClient();
                    wsDefault.MailSend(System.Configuration.ConfigurationManager.AppSettings["mailTo"],
                        clsGlobal.ApplicationName,
                        "<h2>"+clsGlobal.IPAddress()+"</h2>"+ex.Message,
                        "AutoSystem@glsict.com",
                        System.Configuration.ConfigurationManager.AppSettings["siteCode"] + " : " + clsGlobal.ApplicationName,
                        "", "", "", false);
                }
                catch (Exception) { }
            }
        }
        private void tmClient_Tick(object sender, EventArgs e)
        {
            if (refreshSecondCount == 0)
            {
                getServerList();
            }
            else if (refreshSecondCount >= refreshSecond)
            {
                refreshSecondCount = -1;
            }
            refreshSecondCount += 1;
        }
        private bool getAdminChecker()
        {
            #region Variable
            var result = false;
            var clsSQL = new clsSQL(clsGlobal.dbType, clsGlobal.cs);
            var resultSQL = "";
            #endregion
            #region Procedure
            resultSQL = clsSQL.Return("SELECT COUNT(UID) FROM adminlist WHERE LogonName = '"+clsGlobal.WindowsLogonBuilder()+"' AND StatusFlag='A';");
            if (resultSQL != "" && resultSQL != "0")
            {
                result = true;
            }
            #endregion
            return result;
        }
        private void btReport_Click(object sender, EventArgs e)
        {
            Report report = new Report();
            report.ShowDialog(this);
        }
        private void pbDefault_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }
        private void pbDefault_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }
        private void pbDefault_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
        private void btMove_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }
        private void btMove_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }
        private void btMove_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
    }
}
