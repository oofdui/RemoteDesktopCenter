using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RemoteDesktopCenter
{
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();
        }
        private void setDefault()
        {
            #region Variable
            var clsSQL = new clsSQL();
            #endregion
            #region Procedure
            lblFooterDetail.Text = string.Format("Server : {0}", clsSQL.getAppSettingServerName(clsGlobal.cs));
            #endregion
        }
        private void btSearch_Click(object sender, EventArgs e)
        {
            #region Variable
            var clsSQL = new clsSQL(clsGlobal.dbType, clsGlobal.cs);
            var strSQL = new StringBuilder();
            var dt = new DataTable();
            #endregion
            #region Procedure
            #region SQLQuery
            strSQL.Append("");
            strSQL.Append("SELECT ");
            strSQL.Append("ServerName, Domain, Username, TIMESTAMPDIFF(SECOND, ConnectWhen, DisconnectWhen) UsageSecond, TIMESTAMPDIFF(MINUTE, ConnectWhen, DisconnectWhen) UsageMinute ");
            strSQL.Append("FROM ");
            strSQL.Append("sessionlog ");
            strSQL.Append("WHERE ");
            if (dtFrom.Checked)
            {
                strSQL.Append("ConnectWhen>='"+dtFrom.Value.ToString("yyyy-MM-dd HH:mm")+ "' AND ");
            }
            if (dtTo.Checked)
            {
                strSQL.Append("ConnectWhen<='" + dtTo.Value.ToString("yyyy-MM-dd HH:mm") + "' AND ");
            }
            strSQL.Append("StatusFlag = 'A' AND NOT DisconnectWhen IS NULL;");
            #endregion
            dt = clsSQL.Bind(strSQL.ToString());
            if(dt!=null && dt.Rows.Count > 0)
            {
                gvDefault.Visible = true;
                gvDefault.DataSource = dt;
            }
            else
            {
                gvDefault.Visible = false;
                MessageBox.Show("ไม่พบข้อมูลที่ต้องการ", "Not found.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            #endregion
        }
        private void btMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Report_Load(object sender, EventArgs e)
        {
            setDefault();
        }
    }
}
